using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TagManager.MultiTenancy.Dto;

namespace TagManager.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultOutput<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
