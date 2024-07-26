using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Difficulty)
                .WithMany()
                .HasForeignKey(w => w.DifficultyId);

            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Region)
                .WithMany()
                .HasForeignKey(w => w.RegionId);
        }
    }
}
