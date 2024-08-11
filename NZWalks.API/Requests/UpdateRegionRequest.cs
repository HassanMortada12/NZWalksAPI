using MediatR;
using NZWalks.API.Models.Domain;

public class UpdateRegionRequest : IRequest<Region>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    

    public UpdateRegionRequest(int id, Region region)
    {
        Id = id;
        
       
    }
}

