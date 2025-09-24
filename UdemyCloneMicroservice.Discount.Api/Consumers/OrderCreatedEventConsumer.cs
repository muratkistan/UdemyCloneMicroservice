using UdemyCloneMicroservice.Bus.Events;
using UdemyCloneMicroservice.Discount.Api.Features.Discounts;
using UdemyCloneMicroservice.Discount.Api.Repositories;

namespace UdemyCloneMicroservice.Discount.Api.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var discount = new DiscountEntity()
                {
                    Id = NewId.NextSequentialGuid(),
                    Code = DiscountCodeGenerator.Generate(),
                    Created = DateTime.Now,
                    Rate = 0.1f,
                    Expired = DateTime.Now.AddMonths(1),
                    UserId = context.Message.UserId
                };

                await appDbContext.Discounts.AddAsync(discount);

                await appDbContext.SaveChangesAsync();
            }
        }
    }
}