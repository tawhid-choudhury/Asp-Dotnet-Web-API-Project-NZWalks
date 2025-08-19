using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.SqlImplementations
{
    public class SqlRegionsRepository : IRegionsRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SqlRegionsRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            var regions = await _dbContext.Regions.ToListAsync();
            return regions;
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            var region = await _dbContext.Regions.FindAsync(id);
            return region;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region changedRegion)
        {
            // Check if the region with the given id exists
            var existingRegion = await _dbContext.Regions.FindAsync(id);

            // If the region does not exist, return null
            if (existingRegion == null)
            {
                return null; // Region not found
            }

            // Update the existing region's properties
            existingRegion.Code = changedRegion.Code;
            existingRegion.Name = changedRegion.Name;
            existingRegion.RegionImageUrl = changedRegion.RegionImageUrl;

            // Save changes to the database
            await _dbContext.SaveChangesAsync();
            
            return existingRegion; // Return the updated region

        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var existingRegion = await _dbContext.Regions.FindAsync(id);
            if (existingRegion == null)
            {
                return null; // Region not found
            }
            _dbContext.Regions.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }




    }
}
