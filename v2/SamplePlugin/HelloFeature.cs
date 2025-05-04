using FeatureManagerFramework.Interfaces;
using FeatureManagerFramework.Models;
using System.Threading.Tasks;

namespace SamplePlugin
{
    public class HelloFeature : IFeature
    {
        public string Name => "HelloFeature";

        public Task<bool> EvaluateAsync(FeatureContext context)
        {
            // Example: enable only for user 'user123'
            return Task.FromResult(context.UserId == "user123");
        }
    }
}