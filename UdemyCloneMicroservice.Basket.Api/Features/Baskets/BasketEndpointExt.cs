using Asp.Versioning.Builder;
using UdemyCloneMicroservice.Basket.Api.Features.Baskets.AddBasketItem;
using UdemyCloneMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using UdemyCloneMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem;
using UdemyCloneMicroservice.Basket.Api.Features.Baskets.GetBasket;
using UdemyCloneMicroservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint()
            .DeleteBasketItemGroupItemEndpoint()
            .GetBasketGroupItemEndpoint()
            .ApplyDiscountCouponGroupItemEndpoint()
            .RemoveDiscountCouponGroupItemEndpoint().RequireAuthorization();
        }
    }
}