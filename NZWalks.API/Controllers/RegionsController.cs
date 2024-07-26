using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            var regionsDto = regionsDomain.Adapt<List<RegionDto>>();
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var regionDomain = await regionRepository.GetById(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = regionDomain.Adapt<RegionDto>();
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Domain model
            var regionDomainModel = addRegionRequestDto.Adapt<Region>();

            // Create the Region
            regionDomainModel = await regionRepository.Create(regionDomainModel);

            // Map Domain model to DTO
            var regionDto = regionDomainModel.Adapt<RegionDto>();

            // Return Created response
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Domain model
            var regionDomain = updateRegionRequestDto.Adapt<Region>();

            // Update the Region
            regionDomain = await regionRepository.Update(id, regionDomain);
            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map Domain model to DTO
            var regionDto = regionDomain.Adapt<RegionDto>();

            // Return Updated response
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var regionDomain = await regionRepository.Delete(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            // Return No Content response
            return NoContent();
        }
    }
}
