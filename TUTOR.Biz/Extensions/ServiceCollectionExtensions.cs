using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace TUTOR.Biz.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static TConfig GetConfigObject<TConfig>(this IConfiguration configuration)
            where TConfig : class, new()
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var config = new TConfig();

            configuration.Bind(config);

            return config;
        }

        public static TConfig ConfigureConfig<TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TConfig : class, new()
        {
            var config = GetConfigObject<TConfig>(configuration);

            services.AddSingleton(config);

            return config;
        }

        public static void AddApplicationDependency(this IServiceCollection service, string assemblyName, string endsWith)
        {
            var assembly = Assembly.Load(assemblyName);

            var services = assembly.GetTypes().Where(type =>
            type.GetTypeInfo().IsClass && type.Name.EndsWith(endsWith) &&
            !type.GetTypeInfo().IsAbstract);

            foreach (var serviceType in services)
            {
                var allInterfaces = serviceType.GetInterfaces();
                var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));

                foreach (var iServiceType in mainInterfaces)
                {
                    service.AddScoped(iServiceType, serviceType);
                }
            }
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