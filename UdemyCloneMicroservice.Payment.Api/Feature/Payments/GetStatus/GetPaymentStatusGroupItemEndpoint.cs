using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyCloneMicroservice.Shared.Extensions;

namespace UdemyCloneMicroservice.Payment.Api.Feature.Payments.GetStatus
{
    public static class GetPaymentStatusQueryEndpoint
    {
        public static RouteGroupBuilder GetPaymentStatusGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/status/{orderCode}",
                    async ([FromServices] IMediator mediator, string orderCode) =>
                    (await mediator.Send(new GetPaymentStatusRequest(orderCode))).ToGenericResult())
                .WithName("GetPaymentStatus")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .RequireAuthorization("ClientCredential");

            return group;
        }
    }
}