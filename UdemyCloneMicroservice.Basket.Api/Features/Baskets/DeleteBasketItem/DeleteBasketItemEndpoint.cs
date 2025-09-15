﻿using MediatR;
using UdemyCloneMicroservice.Shared.Extensions;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:guid}",
                    async (Guid id, IMediator mediator) =>
                        (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult())
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}