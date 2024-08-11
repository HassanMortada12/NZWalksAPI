using MediatR;
using NZWalks.API.Models.Domain;

public class DeleteRegionRequest : IRequest<Region>
{
    public int Id { get; set; }

    public DeleteRegionRequest(int id)
    {
        Id = id;
    }
}
