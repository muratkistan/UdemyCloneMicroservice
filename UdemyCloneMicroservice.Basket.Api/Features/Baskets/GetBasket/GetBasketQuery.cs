using UdemyCloneMicroservice.Basket.Api.Dto;
using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery : IRequestByServiceResult<BasketDto>;
}