using System.Threading.Tasks;
using BoilerPlate.Configuration.Dto;

namespace BoilerPlate.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
