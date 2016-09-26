using Abp.Authorization;
using TagManager.Authorization.Roles;
using TagManager.MultiTenancy;
using TagManager.Users;

namespace TagManager.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
