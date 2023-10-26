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

        public async Task<NPC> Get(int id)
        {
            var npc = await context.NPCCollection.Include(d => d.Dialogue).FirstOrDefaultAsync(i => i.Id == id);
            if(npc == null) return null;

            return npc;
        }
    }
}
