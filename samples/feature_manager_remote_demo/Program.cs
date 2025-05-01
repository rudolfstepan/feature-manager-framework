using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        var manager = new RemoteFeatureManager(httpClient);

        try
        {
            Console.WriteLine("Loading remote configurations...");
            await manager.LoadConfigurationsAsync("http://localhost:5000/configs"); 
        }
        catch
        {
            Console.WriteLine("Failed to load remote config, using defaults.");
        }

        var context = new ProcessContext
        {
            CustomerTier = "Premium",
            Region = "EU",
            Timestamp = DateTime.Now
        };

        var workflow = manager.ResolveWorkflow("OrderWorkflow", context);
        await workflow.ExecuteAsync(context);
    }
}
