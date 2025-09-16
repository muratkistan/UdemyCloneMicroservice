using Microsoft.AspNetCore.Mvc;
using UdemyCloneMicroservice.Shared.Filters;

namespace UdemyCloneMicroservice.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class CreateDiscountCommandEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateDiscountCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateDiscount")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();

            return group;
        }
    }
}