using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetById(int id);

        Task<Region> Create(Region region);

        Task<Region?> Update(int id, Region region);
        Task<Region?> Delete(int id);
    }
}
