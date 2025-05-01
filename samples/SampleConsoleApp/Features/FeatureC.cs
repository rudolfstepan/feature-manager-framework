using FeatureManagerFramework.Attributes;
using FeatureManagerFramework.Features;
using System;

namespace SampleConsoleApp.Features
{
    [FeatureTag("FeatureC", priority: 3)]
    public class FeatureC : IFeature
    {
        public void Execute()
        {
            Console.WriteLine("Feature C wird ausgeführt.");
        }
    }
}
