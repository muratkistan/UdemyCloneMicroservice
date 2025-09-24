using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Courses.Create
{
    public record CreateCourseCommand(
        string Name,
        string Description,
        decimal Price,
        IFormFile? Picture,
        Guid CategoryId) : IRequestByServiceResult<Guid>;
}