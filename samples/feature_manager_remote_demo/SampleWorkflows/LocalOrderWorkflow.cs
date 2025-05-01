using System;
using System.Threading.Tasks;

[ProcessFeature("OrderWorkflow", Priority = 100, DefaultEnabled = true)]
public class LocalOrderWorkflow : IProcessWorkflow
{
    public async Task ExecuteAsync(ProcessContext context)
    {
        Console.WriteLine("Executing LocalOrderWorkflow:");
        Console.WriteLine("- Capture order");
        await Task.Delay(100);
        Console.WriteLine("- Validate data");
        await Task.Delay(100);
        Console.WriteLine("- Authorize payment");
        await Task.Delay(100);
        Console.WriteLine("- Ship order");
    }
}
