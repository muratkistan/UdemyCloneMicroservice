using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UdemyCloneMicroservice.Order.Application.Contracts.Refit.PaymentService;
using UdemyCloneMicroservice.Order.Application.Contracts.Repositories;
using UdemyCloneMicroservice.Order.Application.UnitOfWork;
using UdemyCloneMicroservice.Order.Domain.Entities;
using UdemyCloneMicroservice.Shared;
using UdemyCloneMicroservice.Shared.Services;

namespace UdemyCloneMicroservice.Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IGenericRepository<int, Address> addressRepository, IIdentityService identityService, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint, IPaymentService paymentService) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!request.Items.Any()) return ServiceResult.Error("Order items not found", "Order must have at least one item", HttpStatusCode.BadRequest);

            var newAddress = new Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };

            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.UserId, request.DiscountRate, newAddress.Id);
            foreach (var orderItem in request.Items) order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);

            order.Address = newAddress;

            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);

            CreatePaymentRequest paymentRequest = new CreatePaymentRequest(order.Code, request.Payment.CardNumber,
            request.Payment.CardHolderName, request.Payment.Expiration, request.Payment.Cvc, order.TotalPrice);

            var paymentResponse = await paymentService.CreateAsync(paymentRequest);

            if (paymentResponse.Status == false)
                return ServiceResult.Error(paymentResponse.ErrorMessage!, HttpStatusCode.InternalServerError);

            order.SetPaidStatus(paymentResponse.PaymentId!.Value);

            var a = paymentResponse.PaymentId;

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            await publishEndpoint.Publish(new Bus.Events.OrderCreatedEvent(order.Id, identityService.UserId), cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}