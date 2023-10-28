using API.Entities.Adventure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class NpcRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public NpcRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<NPC> CreateNpc(NPC npc, int locationId)
        {
            Location l = await context.Locations.Where(l => l.Id == locationId).FirstOrDefaultAsync();
            if (l == null) return null;
            if(l.NPCs == null)
                l.NPCs = new List<NPC>();

            l.NPCs.Add(npc);
            
            return npc;
        }

        public async Task<bool> DeleteNpc(int id)
        {
            var npc = await context.NPCCollection.Where(n => n.Id == id).FirstOrDefaultAsync();
            if(npc == null) return false;

            context.NPCCollection.Remove(npc);
            return true;
        }

        public async Task<NpcDto> Get(int id)
        {
            var npc = await context.NPCCollection.Include(d => d.Dialogue).FirstOrDefaultAsync(i => i.Id == id);
            if(npc == null) return null;


            Dialogue d = await context.DialogueCollection.Include(d => d.ChildResponses).FirstOrDefaultAsync(r => r.Id == npc.Dialogue.Id);
            List<DialogueResponse> responses = new List<DialogueResponse>();
            
            foreach (DialogueResponse r in d.ChildResponses)
                responses.Add(r);

            DialogueDto dialogue = new DialogueDto
            {
                Id = d.Id,
                Text = d.Text,
                Responses = responses
            };

            NpcDto dto = new NpcDto
            {
                Id = npc.Id,
                Name = npc.Name,
                Caption = npc.Caption,
                Dialogue = dialogue
            };


            return dto;
        }
    }
}
