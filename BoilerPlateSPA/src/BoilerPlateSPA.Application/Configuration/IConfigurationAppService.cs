using System.Threading.Tasks;
using BoilerPlateSPA.Configuration.Dto;

namespace BoilerPlateSPA.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
