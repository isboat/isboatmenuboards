using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface IGlobalSettingsService
    {
        void CreateDisplayCode(string account);
        GlobalSettings GetSettings(string account);
    }
}