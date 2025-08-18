using UdemyCloneMicroservice.Catalog.Api.Features.Courses;
using UdemyCloneMicroservice.Catalog.Api.Repositories;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}