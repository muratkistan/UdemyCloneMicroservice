using UdemyCloneMicroservice.Payment.Api.Repositories;

namespace UdemyCloneMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(
       Guid Id,
       string OrderCode,
       string Amount,
       DateTime Created,
       PaymentStatus Status);
}