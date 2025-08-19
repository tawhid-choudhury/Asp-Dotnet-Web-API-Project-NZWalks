using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddWalk([FromBody] WalkAddRequestDto walkAddRequestDto) 
        {
            var walkDomainModel = _mapper.Map<Walk>(walkAddRequestDto);
            await _walksRepository.AddWalk(walkDomainModel);
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

            return Ok(new { Message = "Walk added successfully!" });
        }

        // Get All Walks
        // GET: http://localhost:[portnumber]/api/walks
        [HttpGet]
        public async Task<IActionResult> GetAllWalks() 
        {
            var walkDomainModels = await _walksRepository.GetAllWalks();
            var walkDtos = _mapper.Map<List<WalkDto>>(walkDomainModels);
            return Ok(walkDtos);
        }

        // Get Unique Walk by Id
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetWalkById([FromRoute]int id)
        {
            var walkDomainModel = await _walksRepository.GetWalk(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

    }
}
