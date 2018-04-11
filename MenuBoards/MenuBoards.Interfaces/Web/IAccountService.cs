using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface IAccountService
    {
        object Register(RegisterViewModel model);
    }
}