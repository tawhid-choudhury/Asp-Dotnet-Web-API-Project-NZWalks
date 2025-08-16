using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes it to the base class
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }


        // Define DbSets for each domain model
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


    }
}
