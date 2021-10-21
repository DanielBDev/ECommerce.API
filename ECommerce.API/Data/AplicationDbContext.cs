using ECommerce.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data
{
    public class AplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<DetailEntry> DetailEntries { get; set; }
        public DbSet<DetailSale> DetailSales { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ///DetailEntry
            modelBuilder.Entity<DetailEntry>()
                .HasKey(de => new { de.Id });
            modelBuilder.Entity<DetailEntry>()
                .HasOne(de => de.Entry)
                .WithMany(e => e.DetailEntries).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(de => de.EntryId);
            modelBuilder.Entity<DetailEntry>()
                .HasOne(de => de.Product)
                .WithMany(p => p.DetailEntries).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ds => ds.ProductId);
            ///
            ///DetailSale
            modelBuilder.Entity<DetailSale>()
                .HasKey(ds => new { ds.Id });
            modelBuilder.Entity<DetailSale>()
                .HasOne(ds => ds.Sale)
                .WithMany(s => s.DetailSales).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ds => ds.SaleId);
            modelBuilder.Entity<DetailSale>()
                .HasOne(ds => ds.Product)
                .WithMany(p => p.DetailSales).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ds => ds.ProductId);
            ///
        }
    }
}
