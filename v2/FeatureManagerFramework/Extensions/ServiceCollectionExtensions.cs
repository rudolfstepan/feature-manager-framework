using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FeatureManagerFramework.Interfaces;
using FeatureManagerFramework.Managers;
using FeatureManagerFramework.Providers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureManager(this IServiceCollection services)
        {
            // DirectoryFeatureProvider einmal als Singleton anlegen
            services.AddSingleton<DirectoryFeatureProvider>();
            // Interface und HostedService auf dieselbe Instanz mappen
            services.AddSingleton<IFeatureProvider>(sp => sp.GetRequiredService<DirectoryFeatureProvider>());
            services.AddSingleton<IHostedService>(sp => sp.GetRequiredService<DirectoryFeatureProvider>());

            // FeatureManager
            services.AddSingleton<FeatureManager>();

            return services;
        }
    }
}
