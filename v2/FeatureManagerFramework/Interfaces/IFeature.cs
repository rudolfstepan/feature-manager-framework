using FeatureManagerFramework.Models;
using System.Threading.Tasks;

namespace FeatureManagerFramework.Interfaces
{
    public interface IFeature
    {
        string Name { get; }
        Task<bool> EvaluateAsync(FeatureContext context);
    }
}