using FluffyDuffyMunchkinCats.Models;
using Microsoft.EntityFrameworkCore;

namespace FluffyDuffyMunchkinCats.Data
{
    public class CatContext : DbContext
    {
        public CatContext(DbContextOptions<CatContext> options):
            base(options) { }
        public DbSet<Cat> Cats { get; set; }
    }
}
