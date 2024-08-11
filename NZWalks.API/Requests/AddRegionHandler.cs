using MediatR;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Domain;
using System.Threading;
using System.Threading.Tasks;

public class AddRegionHandler : IRequestHandler<AddRegionRequest, Region>
{
    private readonly IRegionRepository _regionRepository;

    public AddRegionHandler(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public async Task<Region> Handle(AddRegionRequest request, CancellationToken cancellationToken)
    {
        // Map the request data to the domain model
        var regionDomainModel = new Region
        {
            Name = request.Name,
            Code = request.Code
            // Map other properties if needed
        };

        // Create the region in the repository
        var createdRegion = await _regionRepository.Create(regionDomainModel);

        return createdRegion;
    }
}
