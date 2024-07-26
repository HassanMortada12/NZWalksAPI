using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NZWalks.API.Data
{
    public class NZWalksDbContextFactory : IDesignTimeDbContextFactory<NZWalksDbContext>
    {
        public NZWalksDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<NZWalksDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Use Npgsql for PostgreSQL
            optionsBuilder.UseNpgsql(connectionString);

            return new NZWalksDbContext(optionsBuilder.Options);
        }
    }
}
