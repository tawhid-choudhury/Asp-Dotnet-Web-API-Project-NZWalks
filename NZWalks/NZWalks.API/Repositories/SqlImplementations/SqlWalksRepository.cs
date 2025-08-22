using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.SqlImplementations
{
    public class SqlWalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SqlWalksRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Walk> AddWalk(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllWalks(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var walks = _dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Description.Contains(filterQuery));
                }
                else if (filterOn.Equals("Region", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Region.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Difficulty.Name.Contains(filterQuery));
                }

            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false) 
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            //var walks = await _dbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetWalk(Guid id)
        {
            var walk = await _dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x=> x.Id == id);
            return walk;
        }

        public async Task<Walk?> UpdateWalkById(Guid id, Walk walkDomainModel)
        {
            var existingWalk = await _dbContext.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walkDomainModel.Name;
            existingWalk.Description = walkDomainModel.Description;
            existingWalk.LengthInKm = walkDomainModel.LengthInKm;
            existingWalk.WalkImageUrl = walkDomainModel.WalkImageUrl;
            existingWalk.DifficultyId = walkDomainModel.DifficultyId;
            existingWalk.RegionId = walkDomainModel.RegionId;

            await _dbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk?> DeleteWalkById(Guid id)
        {
            var existingWalk =  await _dbContext.Walks.FindAsync(id);
            if (existingWalk == null) 
            {
                return null;
            }
            _dbContext.Walks.Remove(existingWalk);
            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
