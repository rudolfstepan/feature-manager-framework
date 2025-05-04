using FeatureManagerFramework.Interfaces;
using FeatureManagerFramework.Models;
using FeatureManagerFramework.Providers;
using System.Threading.Tasks;

namespace FeatureManagerFramework.Managers
{
    /// <summary>
    /// Uses IFeatureProvider to load features (including plugins) dynamically and evaluate them.
    /// </summary>
    public class FeatureManager
    {
        private readonly IFeatureProvider _provider;

        public FeatureManager(IFeatureProvider provider)
        {
            _provider = provider;
        }

        public async Task<bool> IsEnabledAsync(string name, FeatureContext context)
        {
            foreach (var feature in _provider.GetFeatures())
            {
                if (feature.Name == name)
                    return await feature.EvaluateAsync(context);
            }
            return false;
        }
    }
}