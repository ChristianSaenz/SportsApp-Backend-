using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace SportsApp.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SportsAppDbContext>
    {
        public SportsAppDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddUserSecrets<Program>()
                .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<SportsAppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SportsAppDbContext(optionsBuilder.Options);
        }
    }
}
