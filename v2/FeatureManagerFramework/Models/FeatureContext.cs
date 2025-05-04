using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FeatureManagerFramework.Models
{
    public record FeatureContext
    {
        public string UserId { get; init; }
        public string Environment { get; init; }
        public IReadOnlyDictionary<string, object> Properties { get; init; }

        public FeatureContext(string userId, string environment, IDictionary<string, object> properties)
        {
            UserId = userId;
            Environment = environment;
            Properties = new ReadOnlyDictionary<string, object>(properties ?? new Dictionary<string, object>());
        }
    }
}