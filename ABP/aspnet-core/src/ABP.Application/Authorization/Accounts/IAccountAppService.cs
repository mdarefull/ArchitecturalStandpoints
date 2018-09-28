using System.Threading.Tasks;
using Abp.Application.Services;
using ABP.Authorization.Accounts.Dto;

namespace ABP.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
