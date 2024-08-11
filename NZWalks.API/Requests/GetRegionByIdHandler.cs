using MediatR;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Requests
{
    public class GetRegionByIdHandler : IRequestHandler<GetRegionByIdRequest, Region>
    {
        private readonly IRegionRepository _regionRepository;

        public GetRegionByIdHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<Region> Handle(GetRegionByIdRequest request, CancellationToken cancellationToken)
        {
            return await _regionRepository.GetById(request.Id);
        }
    }
}

