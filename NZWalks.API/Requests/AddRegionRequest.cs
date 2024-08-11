using MediatR;
using NZWalks.API.Models.Domain;

public class AddRegionRequest : IRequest<Region>
{
    public string Name { get; set; }
    public string Code { get; set; }
   

    public AddRegionRequest(string name, string code)
    {
        Name = name;
        Code = code;
        
    }
}
