using UdemyCloneMicroservice.Catalog.Api.Features.Categories;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses.Create;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses.Dtos;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}