using System.Collections.Generic;
using FeatureManagerFramework.Context;
using FeatureManagerFramework.Features;

namespace FeatureManagerFramework.Core
{
    public class FeatureFallbackChain
    {
        private readonly FeatureManager _manager;
        private readonly FeatureContext _context;
        private readonly List<string> _fallbackKeys;

        public FeatureFallbackChain(FeatureManager manager, FeatureContext context)
        {
            _manager = manager;
            _context = context;
            _fallbackKeys = new List<string>();
        }

        public FeatureFallbackChain AddFallback(string key)
        {
            _fallbackKeys.Add(key);
            return this;
        }

        public IFeature? Resolve()
        {
            foreach (var key in _fallbackKeys)
            {
                var feature = _manager.GetFeature(key, _context);
                if (feature != null)
                    return feature;
            }
            return null;
        }
    }
}
