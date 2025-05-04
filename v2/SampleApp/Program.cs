using FeatureManagerFramework.Managers;
using FeatureManagerFramework.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddFeatureManager();
                })
                .Build();

            await host.StartAsync();

            var manager = host.Services.GetRequiredService<FeatureManager>();

            Console.WriteLine("Monitoring HelloFeature status. Place your plugin DLL into the Plugins folder.");
            while (true)
            {
                var ctx = new FeatureContext("user123", "Production", new Dictionary<string, object>());
                bool enabled = await manager.IsEnabledAsync("HelloFeature", ctx);
                Console.WriteLine($"{DateTime.Now}: HelloFeature enabled = {enabled}");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}