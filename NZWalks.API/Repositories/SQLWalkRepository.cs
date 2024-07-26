using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {

        private readonly NZWalksDbContext dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.ToListAsync();
        }

        public async Task<Walk?> GetById(int id)
        {
            return await dbContext.Walks.FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<Walk> Create(Walk walk)
        {
            // Validate if DifficultyId and RegionId exist
            var difficultyExists = await dbContext.Difficulties.AnyAsync(d => d.Id == walk.DifficultyId);
            var regionExists = await dbContext.Regions.AnyAsync(r => r.Id == walk.RegionId);

            if (!difficultyExists || !regionExists)
            {
                throw new ArgumentException("Invalid DifficultyId or RegionId");
            }

            // Add the Walk entity to the database context
            await dbContext.Walks.AddAsync(walk);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> Update(int id, Walk walk)
        {
            // Find the existing walk by ID
            var existingWalk = await dbContext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                // Return null if walk not found
                return null;
            }

            // Validate if DifficultyId and RegionId exist
            var difficultyExists = await dbContext.Difficulties.AnyAsync(d => d.Id == walk.DifficultyId);
            var regionExists = await dbContext.Regions.AnyAsync(r => r.Id == walk.RegionId);

            if (!difficultyExists || !regionExists)
            {
                throw new ArgumentException("Invalid DifficultyId or RegionId");
            }

            // Update properties
            existingWalk.Name = walk.Name;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.Description = walk.Description;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk?> Delete(int id)
        {
            // Find the existing walk by ID
            var existingWalk = await dbContext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                // Return null if walk not found
                return null;
            }

            // Remove the walk from the database context
            dbContext.Walks.Remove(existingWalk);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
