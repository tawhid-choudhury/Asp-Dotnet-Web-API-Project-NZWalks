using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }





        // GET ALL REGIONS
        // GET: http://localhost:[portnumber]/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {

            //STEPS:
            // 1. Get all regions from the database
            // 2. Map the domain model to DTO
            // 3. Return the list of DTOs as a response

            var regionDomains = await _dbContext.Regions.ToListAsync();

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
            //STEPS:
            // 1. Find the region by id from the database
            // 2. If not found, return NotFound
            // 3. Map the domain model to DTO
            // 4. Return the DTO as a response



            var regionDomain = await _dbContext.Regions.FindAsync(id);

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
            //STEPS:
            // 1. Map or convert DTO to Domain Model
            // 2. Use domain model to create a new region
            // 3.  turn back the created region as a DTO
            // 4. Return 201 created response with the created region

            var regionDomainModel = new Region
            {
                Code = regionAddRequestDto.Code,
                Name = regionAddRequestDto.Name,
                RegionImageUrl = regionAddRequestDto.RegionImageUrl
            };

            await _dbContext.Regions.AddAsync(regionDomainModel);
            await _dbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id}, regionDto);
        }





        // UPDATE A REGION
        // PUT: http://localhost:[portnumber]/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RegionUpdateRequestDto updateRequestDto) 
        {
            //STEPS:
            // 1. Check if the region exists by id
            // 2. If not found, return NotFound
            // 3. Map or convert DTO to Domain Model
            // 4. Update the region in the database
            // 5. Return the updated region as a DTO

            var regionDomainModel = await _dbContext.Regions.FindAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.Code = updateRequestDto.Code;
            regionDomainModel.Name = updateRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRequestDto.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            //STEPS:
            // 1. Check if the region exists by id
            // 2. If not found, return NotFound
            // 3. Delete the region from the database
            // 4. Return deleted region in response

            var regionDomainModel = await _dbContext.Regions.FindAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            _dbContext.Regions.Remove(regionDomainModel);
            await _dbContext.SaveChangesAsync();

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
