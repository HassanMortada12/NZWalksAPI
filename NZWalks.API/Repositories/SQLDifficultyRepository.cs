using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Difficulty> Create(Difficulty difficulty)
        {
            await dbContext.Difficulties.AddAsync(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }


        public async Task<Difficulty?> Delete(int id)
        {
            var existingDifficulty = await dbContext.Difficulties.FirstOrDefaultAsync(r => r.Id == id);
            if (existingDifficulty == null)
            {
                return null;
            }

            dbContext.Difficulties.Remove(existingDifficulty); 
            await dbContext.SaveChangesAsync();

            return existingDifficulty;
        }


        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await dbContext.Difficulties.ToListAsync();
        }


        public async Task<Difficulty?> GetById(int id)
        {
            return await dbContext.Difficulties.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Difficulty?> Update(int id, Difficulty difficulty)
        {
            var existingDifficulty = await dbContext.Difficulties.FirstOrDefaultAsync(r => r.Id == id);
            if (existingDifficulty == null)
            {
                return null; 
            }

            // Update properties
            existingDifficulty.Name = difficulty.Name;
            

            await dbContext.SaveChangesAsync();
            return existingDifficulty;
        }
    }
}
