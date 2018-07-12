using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface IDesignSettingsService
    {

        SlideDesignSettings GetDesignSettings(string slideId);

        BaseResponse SaveDesignSettings(SlideDesignSettings settings);

    }
}