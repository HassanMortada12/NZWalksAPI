using MediatR;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Requests
{
    public record GetRegionByIdRequest(int Id) : IRequest<Region>;
}

