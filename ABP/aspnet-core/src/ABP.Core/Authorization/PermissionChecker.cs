using Abp.Authorization;
using ABP.Authorization.Roles;
using ABP.Authorization.Users;

namespace ABP.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
