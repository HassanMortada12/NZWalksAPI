using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using NZWalks.API.Requests;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        
        private readonly IMediator _mediator;
        public RegionsController(IMediator mediator)
        {
           
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionDomain = await _mediator.Send(new GetAllRegionsRequest());
            var regionsDto = regionDomain.Adapt<List<RegionDto>>();
            return Ok(regionsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var regionDomain = await _mediator.Send(new GetRegionByIdRequest(id));
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

            // Map DTO to MediatR request
            var addRegionRequest = addRegionRequestDto.Adapt<AddRegionRequest>();

            // Send the request to MediatR
            var createdRegion = await _mediator.Send(addRegionRequest);

            // Map the created domain model to DTO
            var regionDto = createdRegion.Adapt<RegionDto>();

            // Return Created response
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
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
            regionDomain = await _mediator.Send(new UpdateRegionRequest(id,regionDomain));
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
            // Send the delete request to MediatR
            var deletedRegion = await _mediator.Send(new DeleteRegionRequest(id));

            if (deletedRegion == null)
            {
                return NotFound();
            }

            // Return No Content response
            return NoContent();
        }

    }
}
