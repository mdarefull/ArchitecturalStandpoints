using System.Threading.Tasks;
using ABP.Configuration.Dto;

namespace ABP.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
