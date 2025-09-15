using Asp.Versioning.Builder;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories.GetById;
using UdemyCloneMicroservice.Shared.Filters;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint();
        }
    }
}