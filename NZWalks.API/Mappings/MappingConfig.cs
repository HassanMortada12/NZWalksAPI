using Mapster;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Region, RegionDto>();
        config.NewConfig<AddRegionRequestDto, Region>();
        config.NewConfig<UpdateRegionRequestDto, Region>();
    }
}
