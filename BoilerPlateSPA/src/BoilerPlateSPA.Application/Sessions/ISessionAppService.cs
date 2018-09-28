using System.Threading.Tasks;
using Abp.Application.Services;
using BoilerPlateSPA.Sessions.Dto;

namespace BoilerPlateSPA.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
