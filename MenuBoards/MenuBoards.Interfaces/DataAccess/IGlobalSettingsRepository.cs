using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface IGlobalSettingsRepository
    {

        DisplayCodeResponse VerifyDisplayCode(DisplayCode code);

        void CreateDisplayCode(string account);

        string GetDisplayCode(string account);
    }
}