using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface ILoginService
    {
        UserViewModel LogIn(string username, string password);

        void LogOut();
    }
}