using UdemyCloneMicroservice.Discount.Api.Repositories;

namespace UdemyCloneMicroservice.Discount.Api.Features.Discounts
{
    public class DiscountEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public float Rate { get; set; }
        public string Code { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public DateTime Expired { get; set; }
    }
}