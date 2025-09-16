using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCloneMicroservice.Order.Application.Features.Orders.CreateOrder;
using UdemyCloneMicroservice.Order.Domain.Entities;

namespace UdemyCloneMicroservice.Order.Application.Features.Orders
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}