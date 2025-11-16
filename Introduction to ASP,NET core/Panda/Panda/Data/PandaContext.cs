using Microsoft.EntityFrameworkCore;
using Panda.Models;

namespace Panda.Data
{
    public class PandaContext: DbContext
    {
        public PandaContext(DbContextOptions options): 
            base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>()
                .Property(el => el.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Package>()
                .Property(el=> el.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Receipt>()
               .HasOne(r => r.Recipient)
               .WithMany()
               .HasForeignKey(r => r.RecipientId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Package)
                .WithOne()
                .HasForeignKey<Receipt>(r => r.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
    }
}
