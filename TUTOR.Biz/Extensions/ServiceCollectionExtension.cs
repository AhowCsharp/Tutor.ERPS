using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TUTOR.Biz.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static TConfig ConfigureConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();

            configuration.Bind(config);
            services.AddSingleton(config);

            return config;
        }

        public static IServiceCollection AddScopedByInterface<TInterface>(this IServiceCollection services)
        {
            var items = typeof(TInterface).Assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(typeof(TInterface).Name) == typeof(TInterface));

            foreach (var item in items)
            {
                services.AddScoped(item);
            }

            return services;
        }
    }
}
