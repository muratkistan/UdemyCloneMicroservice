using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;
}