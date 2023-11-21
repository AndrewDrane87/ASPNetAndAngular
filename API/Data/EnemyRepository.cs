using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class EnemyRepository
    {
        private readonly DataContext context;

        public EnemyRepository(DataContext context)
        {
            this.context = context;
        }

        #region Admin Functions

        public async Task<EnemyDto> CreateEnemy(Enemy e, int locationId)
        {
            var location = await context.Locations
                .Include(l => l.EnemyLocationLinks).ThenInclude(link => link.Enemy)
                .FirstOrDefaultAsync(l => l.Id == locationId);
            if (location == null) return null;

            EnemyLocationLink newLink = new EnemyLocationLink()
            {
                Location = location,
                Enemy = e
            };

            location.EnemyLocationLinks.Add(newLink);
            await context.SaveChangesAsync();
            return EnemyDto.Convert(e);
        }

        public async Task<EnemyDto> GetEnemyById(int id)
        {
            var enemy = await context.EnemyCollection
                .Include(e => e.Photo)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enemy == null) return null;
            return EnemyDto.Convert(enemy);
        }

        public async Task<List<EnemyDto>> GetEnemiesByLocation(int locationId)
        {
            var location = await context.Locations
                .Include(l => l.EnemyLocationLinks).ThenInclude(link => link.Enemy).ThenInclude(e => e.Photo)
                .FirstOrDefaultAsync(l => l.Id == locationId);

            if (location == null) return null;

            List<EnemyDto> enemies = new List<EnemyDto>();
            foreach (EnemyLocationLink link in location.EnemyLocationLinks)
                enemies.Add(EnemyDto.Convert(link.Enemy));

            return enemies;
        }

        #endregion

        #region Player Functions

        #endregion


    }
}
