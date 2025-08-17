using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionsRepository _repository;

        public RegionsController(NZWalksDbContext dbContext, IRegionsRepository repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }





        // GET ALL REGIONS
        // GET: http://localhost:[portnumber]/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regionDomains = await _repository.GetAllRegionsAsync();

            var regionDtos = new List<RegionDto>();

            foreach (var region in regionDomains)
            {
                regionDtos.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionDtos);
        }




        // GET SINGLE REGION BY ID
        // GET: http://localhost:[portnumber]/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var regionDomain = await _repository.GetRegionByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);

        }




        // ADD A NEW REGION
        // POST: http://localhost:[portnumber]/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegionAddRequestDto regionAddRequestDto)
        {

            var regionDomainModel = new Region
            {
                Code = regionAddRequestDto.Code,
                Name = regionAddRequestDto.Name,
                RegionImageUrl = regionAddRequestDto.RegionImageUrl
            };

            await _repository.AddRegionAsync(regionDomainModel);

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }





        // UPDATE A REGION
        // PUT: http://localhost:[portnumber]/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RegionUpdateRequestDto updateRequestDto)
        {
            var regionDomainModel = new Region
            {
                Code = updateRequestDto.Code,
                Name = updateRequestDto.Name,
                RegionImageUrl = updateRequestDto.RegionImageUrl
            };

            regionDomainModel = await _repository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }




        // DELETE A REGION
        // DELETE: http://localhost:[portnumber]/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _repository.DeleteRegionAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }
    }
}
