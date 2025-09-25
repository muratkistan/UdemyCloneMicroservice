using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Payment.Api.Feature.Payments.GetStatus
{
    public record GetPaymentStatusRequest(string orderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;
}