using MassTransit;
using UdemyCloneMicroservice.Bus;
using UdemyCloneMicroservice.Discount.Api.Consumers;

namespace UdemyCloneMicroservice.Discount.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services,
            IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<OrderCreatedEventConsumer>();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("discount-microservice.upload-order-created.queue",
                        e => { e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx); });

                    // cfg.ConfigureEndpoints(ctx);
                });
            });

            return services;
        }
    }
}