using MediatR;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NZWalks.API.Requests;

public class GetAllRegionsHandler : IRequestHandler<GetAllRegionsRequest, IEnumerable<Region>>
{
    private readonly IRegionRepository _regionRepository;

    public GetAllRegionsHandler(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public async Task<IEnumerable<Region>> Handle(GetAllRegionsRequest request, CancellationToken cancellationToken)
    {
        return await _regionRepository.GetAllAsync();
    }
}


