﻿using API.Entities.Adventure;
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
            var location = await context.Locations.Include(l=> l.Enemies).FirstOrDefaultAsync(l => l.Id == locationId);
            if (location == null) return null;

            location.Enemies.Add(e);
            await context.SaveChangesAsync();
            return EnemyDto.Convert(e);
        }

        public async Task<EnemyDto> GetEnemyById(int id)
        {
            var enemy = await context.EnemyCollection
                .Include(e=> e.Photo)
                .Include(e=>e.MeleeAttack)
                .Include(e=>e.RangedAttack)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enemy == null) return null;
            return EnemyDto.Convert(enemy);
        }

        public async Task<List<EnemyDto>> GetEnemiesByLocation(int locationId)
        {
            var location = await context.Locations
                .Include(l => l.Enemies).ThenInclude(e => e.Photo)
                .Include(l => l.Enemies).ThenInclude(e => e.MeleeAttack)
                .Include(l => l.Enemies).ThenInclude(e => e.RangedAttack)
                .FirstOrDefaultAsync(l => l.Id == locationId);

            if(location == null) return null;

            List<EnemyDto> enemies = new List<EnemyDto>();
            foreach(Enemy e in  location.Enemies)
                enemies.Add(EnemyDto.Convert(e));

            return enemies;
        }

        #endregion

        #region Player Functions

        #endregion


    }
}
