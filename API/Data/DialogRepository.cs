using API.DTOs.Adventure;
using API.Entities.Adventure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace API.Data
{
    public class DialogRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public DialogRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<DialogueDto> CreateMainDialogue(DialogueDto dialogue, int npcId)
        {

            NPC npc = await context.NPCCollection.Where(n => n.Id == npcId).FirstOrDefaultAsync();
            if (npc == null) return null;

            Dialogue d = new Dialogue { Text = dialogue.Text };
            await context.DialogueCollection.AddAsync(d);
            await context.SaveChangesAsync();

            npc.Dialogue = d;
            return dialogue;
        }

        public async Task<DialogueDto> CreateResponse(DialogueResponse response, int dialogId)
        {
            var d = await context.DialogueCollection.Include(d => d.ChildResponses).FirstOrDefaultAsync(d => d.Id == dialogId);
            if (d == null) return null;


            d.ChildResponses.Add(response);

            return new DialogueDto
            {
                Id = d.Id,
                Text = d.Text,
                Responses = d.ChildResponses,
            };
        }

        public async Task<DialogueDto> CreateChildDialogue(DialogueDto dialogue, int responseId)
        {
            var response = await context.ResponseCollection.FirstOrDefaultAsync(r => r.Id == responseId);
            if (response == null) return null;

            Dialogue d = new Dialogue { Text= dialogue.Text, ParentResponse = response };
            await context.DialogueCollection.AddAsync(d);
            await context.SaveChangesAsync();

            return DialogueDto.Convert(d);
        }

        public async Task<DialogueDto> DeleteResponse(int responseId)
        {
            var response = await context.ResponseCollection.FirstOrDefaultAsync(r => r.Id == responseId);

            var dialogue = await context.DialogueCollection
                .Where(d => d.ChildResponses.Contains(response))
                .Include(d => d.ChildResponses)
                .FirstOrDefaultAsync();

            if (dialogue == null || response == null) return null;

            dialogue.ChildResponses.Remove(response);

            var withParentResponse = await context.DialogueCollection.Where(d => d.ParentResponse == response).FirstOrDefaultAsync();
            if (withParentResponse != null)
                withParentResponse.ParentResponse = null;

            context.ResponseCollection.Remove(response);
            return DialogueDto.Convert(dialogue);
        }

        /// <summary>
        /// This will set what dialog is pulled once a user selects a response
        /// A response to the response if you will
        /// </summary>
        /// <param name="responseId">The response the user selects</param>
        /// <param name="dialogId">The dialog to load once said response is selected</param>
        /// <returns></returns>
        public async Task<DialogueResponse> LinkResponseToDialogue(int responseId, int dialogId)
        {

            DialogueResponse response = await context.ResponseCollection.Where(r => r.Id == responseId).FirstOrDefaultAsync();
            if (response == null) return null;

            Dialogue dialog = await context.DialogueCollection.Where(d => d.Id == dialogId).FirstOrDefaultAsync();
            if (dialog == null) return null;

            //response.Response = dialog;
            return response;
        }

        public async Task<DialogueResponse> CreateDialogue(DialogueDto dialogue, int responseId)
        {
            DialogueResponse response = await context.ResponseCollection.Where(r => r.Id == responseId).FirstOrDefaultAsync();
            if (response == null) return null;

            //response.Response = dialogue;
            return response;
        }

        public async Task<DialogueDto> GetDialogue(int id)
        {
            Dialogue d = await context.DialogueCollection.Include(r => r.ChildResponses).FirstOrDefaultAsync(r => r.Id == id);

            List<DialogueResponse> responses = new List<DialogueResponse>();
            foreach (DialogueResponse r in d.ChildResponses)
                responses.Add(r);

            DialogueDto dto = new DialogueDto
            {
                Id = id,
                Text = d.Text,
                Responses = responses
            };

            return dto;
        }

        public async Task<DialogueDto> GetPreviousDialogue(int id)
        {
            var childDialogue = await context.DialogueCollection.Include(d => d.ParentResponse).FirstOrDefaultAsync(d => d.Id == id);
            if (childDialogue == null) return null;
            
            var parent = await context.ResponseCollection.FirstOrDefaultAsync(r => r == childDialogue.ParentResponse);
            if(parent == null) return null;

            var d = await context.DialogueCollection.Include(d => d.ChildResponses).FirstOrDefaultAsync(d => d.ChildResponses.Contains(parent));
            if(d == null) return null;

            return DialogueDto.Convert(d);
        }

        public async Task<DialogueDto> GetDialogueFromResponse(int responseId)
        {
            Dialogue dialogue = await context.DialogueCollection
                .Include(r => r.ChildResponses).FirstOrDefaultAsync(d => d.ParentResponse.Id == responseId);
            if (dialogue == null) return null;

            return new DialogueDto
            {
                Id = dialogue.Id,
                Text = dialogue.Text,
                Responses = dialogue.ChildResponses
            };
        }
    }
}
