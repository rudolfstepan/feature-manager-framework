using FeatureManagerFramework.Attributes;
using FeatureManagerFramework.Features;
using System;

namespace SampleConsoleApp.Features
{
    [FeatureTag("FeatureB", priority: 2)]
    public class FeatureB : IFeature
    {
        public void Execute()
        {
            Console.WriteLine("Feature B wird ausgeführt.");
        }
    }
}
