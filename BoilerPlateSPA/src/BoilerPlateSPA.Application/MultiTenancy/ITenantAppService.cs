using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BoilerPlateSPA.MultiTenancy.Dto;

namespace BoilerPlateSPA.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
