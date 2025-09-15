using MediatR;
using System.Net;
using System.Text.Json;
using UdemyCloneMicroservice.Basket.Api.Data;
using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(
        BasketService basketService)
        : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<BasketEntity>(basketAsJson);

            var basketItemToDelete = currentBasket!.Items.FirstOrDefault(x => x.Id == request.Id);

            if (basketItemToDelete is null)
            {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);
            }

            currentBasket.Items.Remove(basketItemToDelete);

            basketAsJson = JsonSerializer.Serialize(currentBasket);

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}