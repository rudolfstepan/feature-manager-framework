using System.Collections.Generic;
using FeatureManagerFramework.Interfaces;

namespace FeatureManagerFramework.Providers
{
    public interface IFeatureProvider
    {
        IReadOnlyCollection<IFeature> GetFeatures();
    }
}