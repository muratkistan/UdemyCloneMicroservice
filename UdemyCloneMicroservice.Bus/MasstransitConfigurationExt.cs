using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Bus
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddCommonMasstransitExt(this IServiceCollection services,
            IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ConfigureEndpoints(ctx); // oto queue name

                    //cfg.ReceiveEndpoint("basket-microservice.create-order-event.queue",
                    //    e => { e.ConfigureConsumer<CreateOrderEventConsumer>(context); });
                });
            });

            return services;
        }
    }
}