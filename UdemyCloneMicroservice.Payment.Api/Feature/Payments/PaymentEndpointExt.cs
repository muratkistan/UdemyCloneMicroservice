using Asp.Versioning.Builder;
using UdemyCloneMicroservice.Payment.Api.Feature.Payments.Create;
using UdemyCloneMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;

namespace UdemyCloneMicroservice.Payment.Api.Feature.Payments
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint()
            .GetAllPaymentsByUserIdGroupItemEndpoint().RequireAuthorization("Password");
        }
    }
}