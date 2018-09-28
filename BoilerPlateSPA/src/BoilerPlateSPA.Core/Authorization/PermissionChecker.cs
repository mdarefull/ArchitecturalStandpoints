using Abp.Authorization;
using BoilerPlateSPA.Authorization.Roles;
using BoilerPlateSPA.Authorization.Users;

namespace BoilerPlateSPA.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
