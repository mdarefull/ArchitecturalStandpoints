namespace ArchitecturalStandpoints.Core
{
    public interface IServiceOperationResolver
    {
        ResolveOperationUrlOutput ResolveOperationUrl(ResolveOperationUrlInput input);
    }

    public class ResolveOperationUrlInput
    {
        public string ServiceName { get; set; }
        public string OperationName { get; set; }
        public string[] OperationArgs { get; set; }
    }

    public enum ResolveOperationUrlResults
    {
        Success,
        NotFound,
        ErrorGeneric
    }
    public class ResolveOperationUrlOutput
    {
        public ResolveOperationUrlResults ResultType { get; set; } = ResolveOperationUrlResults.ErrorGeneric;
        public string OperationUrl { get; set; }
    }
}
