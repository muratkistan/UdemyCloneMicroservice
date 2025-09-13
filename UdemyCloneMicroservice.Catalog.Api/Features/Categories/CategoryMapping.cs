using AutoMapper;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories.Dtos;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}