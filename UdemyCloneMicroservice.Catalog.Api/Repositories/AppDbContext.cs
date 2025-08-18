using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses;

namespace UdemyCloneMicroservice.Catalog.Api.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,
                    database.DatabaseNamespace.DatabaseName);

            return new AppDbContext(optionsBuilder.Options);
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}