using API.DTOs.Adventure;
using API.Entities.Adventure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Dialogue> CreateMainDialogue(Dialogue dialogue, int npcId)
        {
         
            NPC npc = await context.NPCCollection.Where(n => n.Id == npcId).FirstOrDefaultAsync();
            if (npc == null) return null;

            Dialogue d = new Dialogue { Text = dialogue.Text };
            await context.DialogueCollection.AddAsync(d);
            await context.SaveChangesAsync();
            
            npc.Dialogue = d;
            return dialogue;
        }

        public async Task<DialogDto> CreateResponse(DialogueResponse response, int dialogId)
        {
            var d = await context.DialogueCollection.Where(d => d.Id == dialogId).Include(d => d.ChildResponses).FirstOrDefaultAsync();
            if (d == null) return null;

            //await context.ResponseCollection.AddAsync(response);
            //await context.SaveChangesAsync();

            d.ChildResponses.Add(new DialogueResponseLink { FromDialogue = d, ToResponse = response });

            List<DialogueResponse> responses = new List<DialogueResponse>();
            foreach(DialogueResponseLink link in d.ChildResponses)
            {
                DialogueResponse r = await context.ResponseCollection.Where(r => r.Id == link.FromResponseId).FirstOrDefaultAsync();
                responses.Add(r);
            }

            return new DialogDto
            {
                Id = d.Id,
                Text = d.Text,
                Responses = responses,
            };
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

        public async Task<DialogueResponse> CreateDialogue(Dialogue dialogue, int responseId)
        {
            DialogueResponse response = await context.ResponseCollection.Where(r => r.Id == responseId).FirstOrDefaultAsync();
            if (response == null) return null;

            //response.Response = dialogue;
            return response;
        }

        public async Task<DialogDto> GetDialogue(int id)
        {
            Dialogue d = await context.DialogueCollection.Include(r => r.ChildResponses).ThenInclude(r => r.ToResponse).FirstOrDefaultAsync(r => r.Id == id);
            
            List<DialogueResponse> responses = new List<DialogueResponse>();
            foreach(DialogueResponseLink l in d.ChildResponses)
                responses.Add(l.ToResponse);

            DialogDto dto = new DialogDto
            {
                Id = id,
                Text = d.Text,
                Responses = responses
            };

            return dto;
        }

        public async Task<DialogDto> GetDialogueFromResponse(int responseId)
        {
            DialogueResponse r = await context.ResponseCollection.Include(r => r.ChildDialogue).FirstOrDefaultAsync(r=>r.Id == responseId);
            if(r == null) return null;



            return await GetDialogue(r.ChildDialogue.ToDialogueId ?? -1);
        }
    }
}
