using FinWise.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinWise.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<FinancialRecord> FinancialRecords => Set<FinancialRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dinheiro: define precisão e escala (ex: 123456789012345.67)
            modelBuilder.Entity<FinancialRecord>()
                .Property(r => r.Amount)
                .HasColumnType("decimal(18,2)");

            // Data: salva só a data (sem hora)
            modelBuilder.Entity<FinancialRecord>()
                .Property(r => r.Date)
                .HasColumnType("date");
        }

    }
}
