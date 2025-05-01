using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FeatureManagerFramework.Attributes;
using FeatureManagerFramework.Features;
using FeatureManagerFramework.Context;

namespace FeatureManagerFramework.Core
{
    public class FeatureManager
    {
        private readonly Dictionary<string, List<Type>> _featureTypes;

        public FeatureManager()
        {
            _featureTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IFeature).IsAssignableFrom(t) &&
                            t.GetCustomAttribute<FeatureTagAttribute>() != null)
                .GroupBy(t => t.GetCustomAttribute<FeatureTagAttribute>()!.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(t => t.GetCustomAttribute<FeatureTagAttribute>()!.Priority).ToList()
                );
        }

        public IFeature? GetFeature(string key, FeatureContext context)
        {
            if (!_featureTypes.ContainsKey(key))
                return null;

            var type = _featureTypes[key].FirstOrDefault();
            return type != null ? (IFeature)Activator.CreateInstance(type)! : null;
        }

        public IEnumerable<string> ListAvailableFeatures() => _featureTypes.Keys;
    }
}
