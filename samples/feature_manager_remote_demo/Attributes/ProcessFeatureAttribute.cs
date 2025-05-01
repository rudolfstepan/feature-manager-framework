using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ProcessFeatureAttribute : Attribute
{
    public string Key { get; }
    public int Priority { get; set; } = 0;
    public bool DefaultEnabled { get; set; } = true;

    public ProcessFeatureAttribute(string key)
    {
        Key = key;
    }
}
