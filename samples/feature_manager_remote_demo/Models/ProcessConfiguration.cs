using System.Collections.Generic;

public class ProcessConfiguration
{
    public string FeatureKey { get; set; }
    public List<Condition> Conditions { get; set; } = new();
    public List<string> Sequence { get; set; } = new();
}
