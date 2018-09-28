using System.Threading.Tasks;
using Abp.Application.Services;
using BoilerPlateSPA.Authorization.Accounts.Dto;

namespace BoilerPlateSPA.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
