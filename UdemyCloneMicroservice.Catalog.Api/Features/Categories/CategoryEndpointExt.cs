using UdemyCloneMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyCloneMicroservice.Shared.Filters;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories")
                .CreateCategoryGroupItemEndpoint();
        }
    }
}