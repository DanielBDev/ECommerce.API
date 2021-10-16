using ECommerce.API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data
{
    public class AplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Lost> Losts { get; set; }
        public DbSet<DetailEntry> DetailEntries { get; set; }
        public DbSet<DetailLost> DetailLosts { get; set; }
        public DbSet<DetailSale> DetailSales { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<DetailCashRegister> DetailCashRegisters { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options)
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
            ///DetailLost
            modelBuilder.Entity<DetailLost>()
            .HasKey(dl => new { dl.Id });
            modelBuilder.Entity<DetailLost>()
                .HasOne(dl => dl.Lost)
                .WithMany(l => l.DetailLosts).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(dl => dl.LostId);
            modelBuilder.Entity<DetailLost>()
                .HasOne(dl => dl.Product)
                .WithMany(p => p.DetailLosts).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(dl => dl.ProductId);
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
            ///DetailCashRegister
            modelBuilder.Entity<DetailCashRegister>()
                .HasKey(dcr => new { dcr.Id });
            modelBuilder.Entity<DetailCashRegister>()
                .HasOne(dcr => dcr.CashRegister)
                .WithMany(cr => cr.DetailCashRegisters).OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(dcr => dcr.CashRegisterId);
            ///
        }
    }
}
