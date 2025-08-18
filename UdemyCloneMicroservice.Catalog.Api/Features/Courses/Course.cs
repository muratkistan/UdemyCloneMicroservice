using MongoDB.Driver.Core.Misc;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories;
using UdemyCloneMicroservice.Catalog.Api.Repositories;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Courses
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime Created { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public Feature Feature { get; set; } = default!;
    }
}