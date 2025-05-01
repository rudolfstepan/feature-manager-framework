using System;
using FeatureManagerFramework.Context;
using FeatureManagerFramework.Core;
using FeatureManagerFramework.Services;

namespace SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Geben Sie den gewünschten Szenario-Namen ein (z.B. 'BetaTest'):");
            string scenario = Console.ReadLine() ?? "Default";

            var context = new FeatureContext(scenario);
            var manager = new FeatureManagerFramework.Core.FeatureManager();

            Console.WriteLine("Verfügbare Features:");
            foreach (var key in manager.ListAvailableFeatures())
            {
                Console.WriteLine($"- {key}");
            }

            Console.WriteLine("Geben Sie den gewünschten Feature-Key ein:");
            string keyInput = Console.ReadLine() ?? "FeatureA";

            var chain = new FeatureFallbackChain(manager, context)
                            .AddFallback(keyInput)
                            .AddFallback("FeatureB")
                            .AddFallback("FeatureA");

            var feature = chain.Resolve();

            if (feature != null)
            {
                FeatureAuditService.LogFeatureUsage(feature.GetType().Name, context.ActiveScenario);
                feature.Execute();
            }
            else
            {
                Console.WriteLine("Kein Feature verfügbar.");
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste zum Beenden...");
            Console.ReadKey();
        }
    }
}
