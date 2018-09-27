using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABP.MultiTenancy.Dto;

namespace ABP.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
