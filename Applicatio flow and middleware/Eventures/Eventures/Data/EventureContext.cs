using Eventures.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Data
{
    public class EventureContext:IdentityDbContext<AppUser>
    {
        public EventureContext(DbContextOptions options):
            base(options) { }

        public DbSet<Event> Events { get; set; }
    }
}
