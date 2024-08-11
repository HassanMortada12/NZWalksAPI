using MediatR;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Domain;
using System.Threading;
using System.Threading.Tasks;

public class DeleteRegionHandler : IRequestHandler<DeleteRegionRequest, Region>
{
    private readonly IRegionRepository _regionRepository;

    public DeleteRegionHandler(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public async Task<Region> Handle(DeleteRegionRequest request, CancellationToken cancellationToken)
    {
        // Delete the region using the repository
        var deletedRegion = await _regionRepository.Delete(request.Id);

        // Return the deleted region (or null if not found)
        return deletedRegion;
    }
}

