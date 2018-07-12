using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Interfaces.Web
{
    public interface IDisplaySettingsService
    {

        DisplaySettings GetDisplaySettings(string slideId);

        BaseResponse SaveDisplaySettings(DisplaySettings settings);

        DisplayCodeResponse VerifyDisplayCode(DisplayCode code);
    }
}