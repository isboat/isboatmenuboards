using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface ISettingsRepository
    {
        SingleColumnSettings GetSingleColumnSettings(string settingsId);

        DesignSettings GetDesignSettings(string slideId);

        BaseResponse SaveDesignSettings(DesignSettings settings);

        DisplaySettings GetDisplaySettings(string slideId);

        BaseResponse SaveDisplaySettings(DisplaySettings settings);
    }
}