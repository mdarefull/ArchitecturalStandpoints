using ArchitecturalStandpoints.Tests;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace ArchitecturalStandpoints.Api
{
    public static class ApplicationRoot
    {
        public static void ConfigureServices(IServiceCollection services) => services
            .AddSingleton<HttpClient>()
            .AddTransient<IDummyService, DummyService>();
    }
}
