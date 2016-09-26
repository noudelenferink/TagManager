using System.Threading.Tasks;
using Abp.Application.Services;
using TagManager.Roles.Dto;

namespace TagManager.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
