using UdemyCloneMicroservice.Discount.Api.Features.Discounts.CreateDiscount;

namespace UdemyCloneMicroservice.Discount.Api.Features.Discounts
{
    public class DiscountMapping : Profile
    {
        public DiscountMapping()
        {
            CreateMap<CreateDiscountCommand, DiscountEntity>();
        }
    }
}