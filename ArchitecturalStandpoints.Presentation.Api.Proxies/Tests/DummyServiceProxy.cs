using ArchitecturalStandpoints.Core;
using ArchitecturalStandpoints.Tests;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArchitecturalStandpoints.Api.Tests
{
    public sealed class DummyServiceProxy : IDummyService
    {
        private IServiceOperationResolver ServiceOperationResolver { get; }
        public DummyServiceProxy(IServiceOperationResolver serviceOperationResolver)
            => ServiceOperationResolver = serviceOperationResolver;

        public async Task<GreetOutput> Greet(GreetInput input)
        {
            var operationUrlResult = ServiceOperationResolver
                                     .ResolveOperationUrl(new ResolveOperationUrlInput
                                     {
                                         ServiceName = nameof(IDummyService),
                                         OperationName = nameof(IDummyService.Greet),
                                         OperationArgs = new[] { nameof(GreetInput) }
                                     });
            if (operationUrlResult.ResultType != ResolveOperationUrlResults.Success)
            {
                return new GreetOutput();
            }

            try
            {
                var httpClient = new HttpClient();
                var url = $"{operationUrlResult.OperationUrl}?{nameof(input.ToWho)}={input.ToWho}";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<GreetOutput>();
                }
                return new GreetOutput();
            }
            catch (HttpRequestException)
            {
                return new GreetOutput();
            }
        }
    }
}
