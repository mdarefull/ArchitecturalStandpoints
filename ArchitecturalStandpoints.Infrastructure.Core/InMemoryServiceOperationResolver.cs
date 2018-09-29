using ArchitecturalStandpoints.Tests;
using System.Collections.Generic;

namespace ArchitecturalStandpoints.Core
{
    public class InMemoryServiceOperationResolver : IServiceOperationResolver
    {
        protected const string GLOBAL_URL = "http://localhost:6001/api";
        protected virtual Dictionary<string, string> RegisteredServices { get; set; }
        = new Dictionary<string, string>
        {
            [nameof(IDummyService)] = $"{GLOBAL_URL}/DummyService",
        };

        public virtual ResolveOperationUrlOutput ResolveOperationUrl(ResolveOperationUrlInput input)
            => RegisteredServices.ContainsKey(input.ServiceName)
            ? new ResolveOperationUrlOutput
            {
                ResultType = ResolveOperationUrlResults.Success,
                OperationUrl = $"{RegisteredServices[input.ServiceName]}/{input.OperationName}",
            }
            : new ResolveOperationUrlOutput
            {
                ResultType = ResolveOperationUrlResults.NotFound
            };
    }
}
