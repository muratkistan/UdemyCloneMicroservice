﻿using AutoMapper;
using MediatR;
using System.Net;
using System.Text.Json;
using UdemyCloneMicroservice.Basket.Api.Data;
using UdemyCloneMicroservice.Basket.Api.Dto;
using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(
        BasketService basketService,
        IMapper mapper)
        : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request,
            CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<BasketEntity>(basketAsString)!;

            var basketDto = mapper.Map<BasketDto>(basket);

            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}