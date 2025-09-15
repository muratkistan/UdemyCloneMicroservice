using MediatR;
using System.Net;
using System.Text.Json;
using UdemyCloneMicroservice.Basket.Api.Data;
using UdemyCloneMicroservice.Basket.Api.Dto;
using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(
         BasketService basketService)
         : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<BasketEntity>(basketAsJson)!;

            if (!basket.Items.Any())
            {
                return ServiceResult<BasketDto>.Error("Basket item not found", HttpStatusCode.NotFound);
            }

            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(basket);

            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}