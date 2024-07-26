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
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;

        public DifficultyController(IDifficultyRepository difficultyRepository)
        {
            this.difficultyRepository = difficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var difficultiesDomain = await difficultyRepository.GetAllAsync();
            var difficultiesDto = difficultiesDomain.Adapt<List<DifficultyDto>>();
            return Ok(difficultiesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var difficultyDomain = await difficultyRepository.GetById(id);
            if (difficultyDomain == null)
            {
                return NotFound();
            }

            var difficultyDto = difficultyDomain.Adapt<DifficultyDto>();
            return Ok(difficultyDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddDifficultyRequestDto addDifficultyRequestDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Domain model
            var difficultyDomainModel = addDifficultyRequestDto.Adapt<Difficulty>();

            // Create the Difficulty
            difficultyDomainModel = await difficultyRepository.Create(difficultyDomainModel);

            // Map Domain model to DTO
            var difficultyDto = difficultyDomainModel.Adapt<DifficultyDto>();

            // Return Created response
            return CreatedAtAction(nameof(GetById), new { id = difficultyDomainModel.Id }, difficultyDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDifficultyRequestDto updateDifficultyRequestDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Domain model
            var difficultyDomain = updateDifficultyRequestDto.Adapt<Difficulty>();

            // Update the Difficulty
            difficultyDomain = await difficultyRepository.Update(id, difficultyDomain);
            if (difficultyDomain == null)
            {
                return NotFound();
            }

            // Map Domain model to DTO
            var difficultyDto = difficultyDomain.Adapt<DifficultyDto>();

            // Return Updated response
            return Ok(difficultyDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var difficultyDomain = await difficultyRepository.Delete(id);
            if (difficultyDomain == null)
            {
                return NotFound();
            }

            // Return No Content response
            return NoContent();
        }
    }
}

