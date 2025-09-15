using Asp.Versioning.Builder;
using UdemyCloneMicroservice.Discount.Api.Features.Discounts.CreateDiscount;
using UdemyCloneMicroservice.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace UdemyCloneMicroservice.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("discounts").WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItemEndpoint()
            .GetDiscountByCodeGroupItemEndpoint();
        }
    }
}