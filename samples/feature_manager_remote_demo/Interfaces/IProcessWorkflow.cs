using System.Threading.Tasks;

public interface IProcessWorkflow
{
    Task ExecuteAsync(ProcessContext context);
}
