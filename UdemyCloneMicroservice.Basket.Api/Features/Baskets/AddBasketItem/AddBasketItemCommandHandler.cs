using MediatR;
using System.Text.Json;
using UdemyCloneMicroservice.Basket.Api.Data;
using UdemyCloneMicroservice.Shared;
using UdemyCloneMicroservice.Shared.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(
        IIdentityService identityService,
        BasketService basketService)
        : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            BasketEntity? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl,
                request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                currentBasket = new BasketEntity(identityService.GetUserId, [newBasketItem]);
                await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<BasketEntity>(basketAsJson);

            var existingBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);

            if (existingBasketItem is not null)
            {
                currentBasket.Items.Remove(existingBasketItem);
            }

            currentBasket.Items.Add(newBasketItem);

            currentBasket.ApplyAvailableDiscount();

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}