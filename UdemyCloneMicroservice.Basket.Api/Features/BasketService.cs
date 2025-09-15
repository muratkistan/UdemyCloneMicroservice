using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UdemyCloneMicroservice.Basket.Api.Const;
using UdemyCloneMicroservice.Basket.Api.Data;
using UdemyCloneMicroservice.Shared.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UdemyCloneMicroservice.Basket.Api.Features
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {
        private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

        public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
        {
            return distributedCache.GetStringAsync(GetCacheKey(), token: cancellationToken);
        }

        public async Task CreateBasketCacheAsync(BasketEntity basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, token: cancellationToken);
        }
    }
}