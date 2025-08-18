using Microsoft.Extensions.Options;

namespace UdemyCloneMicroservice.Catalog.Api.Options
{
    public static class OptionExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<MongoOption>(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);

            return services;
        }
    }
}