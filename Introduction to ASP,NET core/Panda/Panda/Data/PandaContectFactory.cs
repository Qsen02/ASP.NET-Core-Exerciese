using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Panda.Data
{
    public class PandaContextFactory : IDesignTimeDbContextFactory<PandaContext>
    {
        public PandaContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<PandaContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new PandaContext(optionsBuilder.Options);
        }
    }
}