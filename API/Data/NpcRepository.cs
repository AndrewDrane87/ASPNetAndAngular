using API.Entities;
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
            if (l.NPCs == null)
                l.NPCs = new List<NPC>();

            l.NPCs.Add(npc);

            return npc;
        }

        public async Task<bool> DeleteNpc(int id)
        {
            var npc = await context.NPCs.Where(n => n.Id == id).FirstOrDefaultAsync();
            if (npc == null) return false;

            context.NPCs.Remove(npc);
            return true;
        }

        public async Task<NpcDto> Get(int id)
        {
            var npc = await context.NPCs.Include(d => d.Dialogue).FirstOrDefaultAsync(i => i.Id == id);
            if (npc == null) return null;

            NpcDto dto = new NpcDto
            {
                Id = npc.Id,
                Name = npc.Name,
                Caption = npc.Caption,
            };

            if (npc.Dialogue != null)
            {
                DialogueNode d = await context.DialogueNodes
                    .Include(d => d.ToDialogueLinks).ThenInclude(l=> l.ToDialogue)
                    .FirstOrDefaultAsync(r => r.Id == npc.Dialogue.Id);
                
                dto.Dialogue = DialogueNodeDto.Convert(d);
            }
            
            return dto;
        }
    }
}
