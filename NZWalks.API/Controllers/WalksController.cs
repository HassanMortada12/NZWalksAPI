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
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;

        public WalksController(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomain = await walkRepository.GetAllAsync();
            var walksDto = walksDomain.Adapt<List<WalkDto>>();
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var walkDomain = await walkRepository.GetById(id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDto = walkDomain.Adapt<WalkDto>();
            return Ok(walkDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Domain model
            var walkDomainModel = addWalkRequestDto.Adapt<Walk>();


            try
            {
                // Create the Walk
                walkDomainModel = await walkRepository.Create(walkDomainModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            // Map Domain model to DTO
            var walkDto = walkDomainModel.Adapt<WalkDto>();


            // Return Created response
            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Domain model
            var walkDomain = updateWalkRequestDto.Adapt<Walk>();

            // Update the Region
            walkDomain = await walkRepository.Update(id, walkDomain);
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Map Domain model to DTO
            var walkDto = walkDomain.Adapt<WalkDto>();

            // Return Updated response
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var walkDomain = await walkRepository.Delete(id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Return No Content response
            return NoContent();
        }

    }
}
