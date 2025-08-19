using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
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

        public async Task<List<Walk>> GetAllWalks()
        {
            var walks = await _dbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();
            return walks;
        }

        public async Task<Walk?> GetWalk(int id)
        {
            var walk = await _dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x=> x.Id == id);
            return walk;
        }
    }
}
