using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;
}