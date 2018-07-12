using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface ISettingsRepository
    {
        #region Design Settings

        
        SlideDesignSettings GetDesignSettings(string slideId);

        BaseResponse SaveDesignSettings(SlideDesignSettings settings);

        #endregion

        #region Display Settings

        DisplaySettings GetDisplaySettings(string slideId);

        BaseResponse SaveDisplaySettings(DisplaySettings settings);

        #endregion

        DisplayCodeResponse VerifyDisplayCode(DisplayCode code);
    }
}