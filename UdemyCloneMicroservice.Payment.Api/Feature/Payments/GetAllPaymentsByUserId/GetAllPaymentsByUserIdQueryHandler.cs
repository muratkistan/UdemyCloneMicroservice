using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyCloneMicroservice.Payment.Api.Repositories;
using UdemyCloneMicroservice.Shared;
using UdemyCloneMicroservice.Shared.Services;

namespace UdemyCloneMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public class GetAllPaymentsByUserIdQueryHandler(AppDbContext context, IIdentityService identityService)
       : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(
            GetAllPaymentsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;

            var payments = await context.Payments
                .Where(x => x.UserId == userId)
                .Select(x => new GetAllPaymentsByUserIdResponse(
                    x.Id,
                    x.OrderCode,
                    x.Amount.ToString("C"),
                    x.Created,
                    x.Status))
                .ToListAsync(cancellationToken: cancellationToken);

            return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOk(payments);
        }
    }
}