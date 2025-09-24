using MediatR;
using System.Security.Claims;
using UdemyCloneMicroservice.Payment.Api.Repositories;
using UdemyCloneMicroservice.Shared;
using UdemyCloneMicroservice.Shared.Services;

namespace UdemyCloneMicroservice.Payment.Api.Feature.Payments.Create
{
    public class CreatePaymentCommandHandler(AppDbContext appDbContext, IIdentityService identityService, IHttpContextAccessor httpContextAccessor)
       : IRequestHandler<CreatePaymentCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var claims = httpContextAccessor.HttpContext?.User.Claims;

            var userId2 = identityService.UserId;
            var userName = identityService.UserName;
            var roles = identityService.Roles;

            var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber,
                request.CardHolderName,
                request.CardExpirationDate, request.CardSecurityNumber, request.Amount);

            if (!isSuccess)
            {
                return ServiceResult<Guid>.Error("Payment Failed", errorMessage!, System.Net.HttpStatusCode.BadRequest);
            }

            var userId = identityService.UserId;
            var newPayment = new Repositories.Payment(userId, request.OrderCode, request.Amount);
            newPayment.SetStatus(Repositories.PaymentStatus.Success);

            appDbContext.Payments.Add(newPayment);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsOk(newPayment.Id);
        }

        private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber,
            string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
        {
            await Task.Delay(1000);
            return (true, null);

            //return (false,"Payment failed.");
        }
    }
}