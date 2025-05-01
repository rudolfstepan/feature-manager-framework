using System;
using System.Collections.Generic;

namespace FeatureManagerFramework.Services
{
    public static class FeatureAuditService
    {
        private static readonly List<string> _auditLog = new();

        public static void LogFeatureUsage(string featureKey, string contextScenario)
        {
            var logEntry = $"{DateTime.UtcNow}: Feature '{featureKey}' used in scenario '{contextScenario}'";
            _auditLog.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public static IEnumerable<string> GetAuditTrail() => _auditLog.AsReadOnly();
    }
}
