using MassTransit;
using UdemyCloneMicroservice.Basket.Api.Consumers;
using UdemyCloneMicroservice.Bus;
using UdemyCloneMicroservice.Bus.Events;

namespace UdemyCloneMicroservice.Basket.Api
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

                    cfg.ReceiveEndpoint("basket-microservice.upload-order-created.queue",
                        e => { e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx); });

                    // cfg.ConfigureEndpoints(ctx);
                });
            });

            return services;
        }
    }
}