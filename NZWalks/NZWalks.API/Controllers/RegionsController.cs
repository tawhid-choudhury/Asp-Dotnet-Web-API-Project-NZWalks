using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;
using NZWalks.API.Repositories.SqlImplementations;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionsRepository _repository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: http://localhost:[portnumber]/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            var regionDomains = await _repository.GetAllRegionsAsync();
            return Ok(_mapper.Map<List<RegionDto>>(regionDomains));
        }

        // GET SINGLE REGION BY ID
        // GET: http://localhost:[portnumber]/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var regionDomain = await _repository.GetRegionByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDto>(regionDomain));

        }

        // ADD A NEW REGION
        // POST: http://localhost:[portnumber]/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] RegionAddRequestDto regionAddRequestDto)
        {
            var regionDomainModel = _mapper.Map<Region>(regionAddRequestDto);

            await _repository.AddRegionAsync(regionDomainModel);

            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDomainModel.Id }, regionDto);
        }

        // UPDATE A REGION
        // PUT: http://localhost:[portnumber]/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegionById([FromRoute] Guid id, [FromBody] RegionUpdateRequestDto updateRequestDto)
        {
            var regionDomainModel = _mapper.Map<Region>(updateRequestDto);
            regionDomainModel = await _repository.UpdateRegionAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }

        // DELETE A REGION
        // DELETE: http://localhost:[portnumber]/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> DeleteRegionById([FromRoute] Guid id)
        {
            var regionDomainModel = await _repository.DeleteRegionAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);

        }
    }
}
