using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Eventures.Data
{
    public class EventureContextFactory: IDesignTimeDbContextFactory<EventureContext>
    {
        public EventureContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<EventureContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new EventureContext(optionsBuilder.Options);
        }
    }
}
