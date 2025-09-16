using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCloneMicroservice.Order.Application.Contracts.Repositories;

namespace UdemyCloneMicroservice.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
        public Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId)
        {
            return context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == buyerId).OrderByDescending(x => x.Created).ToListAsync();
        }
    }
}