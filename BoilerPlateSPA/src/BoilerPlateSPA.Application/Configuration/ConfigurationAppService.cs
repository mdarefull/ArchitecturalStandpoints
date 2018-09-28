using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BoilerPlateSPA.Configuration.Dto;

namespace BoilerPlateSPA.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BoilerPlateSPAAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
