using System.Threading.Tasks;
using Abp.Application.Services;
using ABP.Sessions.Dto;

namespace ABP.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
