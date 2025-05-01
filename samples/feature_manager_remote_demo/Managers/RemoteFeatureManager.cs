using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

public class RemoteFeatureManager
{
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, ProcessConfiguration> _configs = new();

    public RemoteFeatureManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task LoadConfigurationsAsync(string configUrl)
    {
        var response = await _httpClient.GetStringAsync(configUrl);
        var list = JsonSerializer.Deserialize<List<ProcessConfiguration>>(response)!;
        foreach (var cfg in list)
        {
            _configs[cfg.FeatureKey] = cfg;
        }
    }

    public IProcessWorkflow ResolveWorkflow(string featureKey, ProcessContext context)
    {
        if (_configs.TryGetValue(featureKey, out var cfg))
        {
            bool match = cfg.Conditions.All(c => EvaluateCondition(c, context));
            if (match)
            {
                var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                            let attr = t.GetCustomAttribute<ProcessFeatureAttribute>()
                            where attr != null && attr.Key == featureKey && attr.DefaultEnabled
                            orderby attr.Priority descending
                            select t;

                var type = types.FirstOrDefault();
                if (type != null)
                    return (IProcessWorkflow)Activator.CreateInstance(type)!;
            }
        }

        var localTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                         let attr = t.GetCustomAttribute<ProcessFeatureAttribute>()
                         where attr != null && attr.Key == featureKey && attr.DefaultEnabled
                         orderby attr.Priority descending
                         select t;

        var localType = localTypes.FirstOrDefault();
        if (localType != null)
            return (IProcessWorkflow)Activator.CreateInstance(localType)!;

        throw new InvalidOperationException($"No workflow found for {featureKey}");
    }

    private bool EvaluateCondition(Condition c, ProcessContext context)
    {
        return c.Type switch
        {
            "CustomerTier" => context.CustomerTier == c.Value,
            "Region" => context.Region == c.Value,
            "TimeWindow" => EvaluateTimeWindow(c, context.Timestamp),
            _ => false,
        };
    }

    private bool EvaluateTimeWindow(Condition c, DateTime timestamp)
    {
        if (TimeSpan.TryParse(c.From, out var from) && TimeSpan.TryParse(c.To, out var to))
        {
            var time = timestamp.TimeOfDay;
            return time >= from && time <= to;
        }
        return false;
    }
}
