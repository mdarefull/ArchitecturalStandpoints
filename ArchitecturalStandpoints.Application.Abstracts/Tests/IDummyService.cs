using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Tests
{
    public interface IDummyService
    {
        Task<GreetOutput> Greet(GreetInput input);
    }

    public class GreetInput
    {
        public string ToWho { get; set; }
    }

    public enum GreetResults
    {
        Success,
        ErrorGeneric
    }
    public class GreetOutput
    {
        public GreetResults ResultType { get; set; } = GreetResults.ErrorGeneric;
        public string Message { get; set; }
    }
}
