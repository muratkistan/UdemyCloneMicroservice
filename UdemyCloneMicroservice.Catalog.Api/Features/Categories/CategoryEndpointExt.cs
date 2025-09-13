using UdemyCloneMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories.GetById;
using UdemyCloneMicroservice.Shared.Filters;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint();
        }
    }
}