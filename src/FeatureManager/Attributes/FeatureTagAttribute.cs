using System;

namespace FeatureManagerFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FeatureTagAttribute : Attribute
    {
        public string Key { get; }
        public int Priority { get; }

        public FeatureTagAttribute(string key, int priority = 0)
        {
            Key = key;
            Priority = priority;
        }
    }
}
