using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync();

        Task<Walk?> GetById(int id);

        Task<Walk> Create(Walk walk);

        Task<Walk?> Update(int id, Walk walk);
        Task<Walk?> Delete(int id);
    }

}
