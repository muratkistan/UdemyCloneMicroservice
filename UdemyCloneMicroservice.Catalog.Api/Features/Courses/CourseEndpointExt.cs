using Asp.Versioning.Builder;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses.Create;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses.Delete;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses.GetAll;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses.GetAllByUserId;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses.GetById;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses.Update;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses").WithTags("Courses").WithApiVersionSet(apiVersionSet)
                .CreateCourseGroupItemEndpoint()
            .GetAllCourseGroupItemEndpoint()
            .GetByIdCourseGroupItemEndpoint()
            .UpdateCourseGroupItemEndpoint()
            .DeleteCourseGroupItemEndpoint()
            .GetByUserIdCourseGroupItemEndpoint();
        }
    }
}