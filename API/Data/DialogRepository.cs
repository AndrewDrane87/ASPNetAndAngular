using API.Entities;
using API.Helpers;
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

        public async Task<DialogueNodeDto> CreateMainDialogue(DialogueNodeDto dialogue, int npcId)
        {

            NPC npc = await context.NPCs.FirstOrDefaultAsync(n => n.Id == npcId);
            npc.Dialogue = new DialogueNode { Text = dialogue.Text};
            await context.SaveChangesAsync();
            return DialogueNodeDto.Convert(npc.Dialogue);
        }

        public async Task<DialogueNodeDto> CreateResponse(DialogueNode response, int dialogId)
        {
            var d = await context.DialogueNodes
                .Include(d => d.ToDialogueLinks)
                .FirstOrDefaultAsync(d => d.Id == dialogId);
            if (d == null) return null;


            d.ToDialogueLinks.Add(new DialogueLink { ToDialogue = response, FromDialogue = d});

            return DialogueNodeDto.Convert(d);
        }

        public async Task<DialogueNodeDto> CreateDialogue(DialogueNodeDto dialogue, int fromDialogueId)
        {
            var fromDialogue = await context.DialogueNodes
                .Include(d => d.ToDialogueLinks)
                .FirstOrDefaultAsync(r => r.Id == fromDialogueId);

            if (fromDialogue == null) return null;

            DialogueNode newDialogue = new DialogueNode { Text = dialogue.Text };
            
            fromDialogue.ToDialogueLinks.Add(new DialogueLink { FromDialogue = fromDialogue, ToDialogue = newDialogue });
            await context.SaveChangesAsync();
            
            return DialogueNodeDto.Convert(newDialogue);
        }

        public async Task<StatusMessage> DeleteResponse(int responseId)
        {
            var response = await context.DialogueNodes.FirstOrDefaultAsync(r => r.Id == responseId);
            if(response == null) return new StatusMessage { Status = false, Message = "Could not find node" };
            
            context.DialogueNodes.Remove(response);

            return new StatusMessage { Status = true };
        }

        /*
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

            DialogueNode dialog = await context.DialogueNodes.Where(d => d.Id == dialogId).FirstOrDefaultAsync();
            if (dialog == null) return null;

            //response.Response = dialog;
            return response;
        }
        
        public async Task<DialogueResponse> CreateDialogue(DialogueNodeDto dialogue, int responseId)
        {
            DialogueResponse response = await context.ResponseCollection.Where(r => r.Id == responseId).FirstOrDefaultAsync();
            if (response == null) return null;

            //response.Response = dialogue;
            return response;
        }
        */

        public async Task<DialogueNodeDto> GetDialogue(int id)
        {
            DialogueNode d = await context.DialogueNodes
                .Include(d => d.ToDialogueLinks).ThenInclude(l => l.ToDialogue)
                .FirstOrDefaultAsync(r => r.Id == id);

            return DialogueNodeDto.Convert(d);
        }

        
        /*
        public async Task<DialogueDto> GetPreviousDialogue(int id)
        {
            var childDialogue = await context.DialogueCollection.Include(d => d.ParentResponses).ThenInclude(r => r.).FirstOrDefaultAsync(d => d.Id == id);
            if (childDialogue == null) return null;

            var parent = await context.ResponseCollection.FirstOrDefaultAsync(r => r == childDialogue.ParentResponse);
            if (parent == null) return null;

            var d = await context.DialogueCollection.Include(d => d.ChildResponses).FirstOrDefaultAsync(d => d.ChildResponses.Contains(parent));
            if (d == null) return null;

            return DialogueDto.Convert(d);
        }
        */

        public async Task<DialogueNodeDto> GetDialogueFromResponse(int responseId)
        {
            var responseLink = await context.DialogueLinks
                .Include(link => link.ToDialogue).ThenInclude(d => d.ToDialogueLinks).ThenInclude(link => link.ToDialogue)
                .FirstOrDefaultAsync( link => link.FromDialogueId == responseId);
            
            
            if (responseLink == null) return null;

            return DialogueNodeDto.Convert(responseLink.ToDialogue);
        }
    }
}
