using FeatureManagerFramework.Interfaces;
using FeatureManagerFramework.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace FeatureManagerFramework.Decorators
{
    public class CachingFeatureDecorator : IFeature
    {
        private readonly IFeature _inner;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _duration;

        public CachingFeatureDecorator(IFeature inner, IMemoryCache cache, TimeSpan duration)
        {
            _inner = inner;
            _cache = cache;
            _duration = duration;
        }

        public string Name => _inner.Name;

        public async Task<bool> EvaluateAsync(FeatureContext context)
        {
            var key = $"{Name}-{context.UserId}-{context.Environment}";
            if (_cache.TryGetValue(key, out bool enabled))
            {
                return enabled;
            }
            enabled = await _inner.EvaluateAsync(context);
            _cache.Set(key, enabled, _duration);
            return enabled;
        }
    }
}