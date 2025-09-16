using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCloneMicroservice.Order.Application.Contracts.Repositories;
using UdemyCloneMicroservice.Order.Application.Features.Orders.CreateOrder;
using UdemyCloneMicroservice.Shared;
using UdemyCloneMicroservice.Shared.Services;

namespace UdemyCloneMicroservice.Order.Application.Features.Orders.GetOrders
{
    public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByBuyerId(identityService.GetUserId);

            var response = orders.Select(o => new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();

            return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
        }
    }
}