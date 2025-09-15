using Microsoft.EntityFrameworkCore;

namespace UdemyCloneMicroservice.Payment.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.OrderCode).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Created).IsRequired();
                entity.Property(e => e.Amount).IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.Status).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Payment> Payments { get; set; }
    }
}