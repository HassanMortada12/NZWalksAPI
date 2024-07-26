using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetById(int id);

        Task<Difficulty> Create(Difficulty difficulty);

        Task<Difficulty?> Update(int id, Difficulty difficulty);
        Task<Difficulty?> Delete(int id);
    }
}
