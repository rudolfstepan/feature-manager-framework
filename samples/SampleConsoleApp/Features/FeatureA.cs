using FeatureManagerFramework.Attributes;
using FeatureManagerFramework.Features;
using System;

namespace SampleConsoleApp.Features
{
    [FeatureTag("FeatureA", priority: 1)]
    public class FeatureA : IFeature
    {
        public void Execute()
        {
            Console.WriteLine("Feature A wird ausgeführt.");
        }
    }
}
