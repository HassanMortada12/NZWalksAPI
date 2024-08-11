using MediatR;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Domain;
using System.Threading;
using System.Threading.Tasks;

public class UpdateRegionHandler : IRequestHandler<UpdateRegionRequest, Region>
{
    private readonly IRegionRepository _regionRepository;

    public UpdateRegionHandler(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public async Task<Region> Handle(UpdateRegionRequest request, CancellationToken cancellationToken)
    {
        // Fetch the existing region from the repository
        var existingRegion = await _regionRepository.GetById(request.Id);

        if (existingRegion == null)
        {
            return null; // Or throw an exception, depending on your error handling strategy
        }

        // Update the region properties
        existingRegion.Name = request.Name;
        existingRegion.Code = request.Code;
        // Update other properties as needed

        // Save changes to the repository
        await _regionRepository.Update(request.Id,existingRegion);

        return existingRegion;
    }
}

