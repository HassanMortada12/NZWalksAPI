using MediatR;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Requests
{

    public class GetAllRegionsRequest : IRequest<IEnumerable<Region>>
    {

    }


}
