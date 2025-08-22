using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepository _walksRepository;

        public WalksController(IMapper mapper, IWalksRepository walksRepository)
        {
            _mapper = mapper;
            _walksRepository = walksRepository;
        }

        //Create Walk
        // POST: http://localhost:[portnumber]/api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddWalk([FromBody] WalkAddRequestDto walkAddRequestDto) 
        {
            var walkDomainModel = _mapper.Map<Walk>(walkAddRequestDto);
            await _walksRepository.AddWalk(walkDomainModel);
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

            return CreatedAtAction(nameof(GetWalkById), new {walkDomainModel.Id},walkDto);
        }

        // Get All Walks
        // GET: http://localhost:[portnumber]/api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllWalks(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10) 
        {
            var walkDomainModels = await _walksRepository.GetAllWalks(
                filterOn,
                filterQuery,
                sortBy,
                isAscending,
                pageNumber,
                pageSize);
            var walkDtos = _mapper.Map<List<WalkDto>>(walkDomainModels);
            return Ok(walkDtos);
        }

        // Get Unique Walk by Id
        // GET: http://localhost:[portnumber]/api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute]Guid id)
        {
            var walkDomainModel = await _walksRepository.GetWalk(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        // Update Existing Walk By Id
        // POST: http://localhost:[portnumber]/api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalkById([FromRoute] Guid id, [FromBody] WalkUpdateRequestDto walkUpdateRequestDto) 
        {
            var walkDomainModel = _mapper.Map<Walk>(walkUpdateRequestDto);
            walkDomainModel = await _walksRepository.UpdateWalkById(id, walkDomainModel);

            if (walkDomainModel == null) 
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }


        // Delete Existing Walk
        // DELETE: http://localhost:[portnumber]/api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkById([FromRoute] Guid id) 
        {
            var deletedWalk = await _walksRepository.DeleteWalkById(id);
            if (deletedWalk == null) 
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<WalkDto>(deletedWalk);
            return Ok(walkDto);
        }

    }
}
