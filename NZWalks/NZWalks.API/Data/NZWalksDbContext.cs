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

        // Override OnModelCreating to configure the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data for Difficulties
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("ddfcaa74-c699-4b83-a2c1-784774a5f0c8"),
                    Name = "Easy",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("adb3153d-eed1-43bd-b366-24593d935b6e"),
                    Name = "Medium",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("6d044182-2403-493c-a016-32646a501e82"),
                    Name = "Hard",
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed initial data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                     Id = Guid.Parse("b1c8f0d2-3e4f-4c5a-9b6d-7e8f9a0b1c2d"),
                     Code = "NI",
                     Name = "North Island",
                     RegionImageUrl = "https://images.unsplash.com/photo-1708762841200-04cb847d23f2?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Region()
                {
                    Id = Guid.Parse("c2d3e4f5-6a7b-8c9d-a0b1-c2d3e4f5a6b7"),
                    Code = "SI",
                    Name = "South Island",
                    RegionImageUrl = "https://plus.unsplash.com/premium_photo-1661887966066-c9156b05df1e?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Region()
                {
                    Id = Guid.Parse("2f4b6c8d-1e2f-3a4b-5c6d-7e8f9a0b1c2d"),
                    Code = "AKL",
                    Name = "Auckland",
                    RegionImageUrl = "https://plus.unsplash.com/premium_photo-1682449558329-b04c01db4d42?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                 },
                new Region()
                {
                    Id = Guid.Parse("4e6f8a0b-2c4d-6e8f-0a2b-4c6d8e0f1a2b"),
                    Code = "WLG",
                    Name = "Wellington",
                    RegionImageUrl = "https://plus.unsplash.com/premium_photo-1733266928479-65317ed1f4a9?q=80&w=1169&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Region()
                {
                    Id = Guid.Parse("6a8b0c2d-4e6f-8a0b-c2d4-e6f8a0b2c4d6"),
                    Code = "QZN",
                    Name = "Queenstown",
                    RegionImageUrl = "https://plus.unsplash.com/premium_photo-1661882021629-2b0888d93c94?q=80&w=1632&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Region()
                {
                    Id = Guid.Parse("8c0d2e4f-6a8b-0c2d-4e6f-8a0b2c4d6e8f"),
                    Code = "ROT",
                    Name = "Rotorua",
                    RegionImageUrl = "https://plus.unsplash.com/premium_photo-1665908672979-0d53809ecc1a?q=80&w=1632&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
