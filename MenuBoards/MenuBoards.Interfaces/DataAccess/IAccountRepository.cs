using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface IAccountRepository
    {
        BaseResponse Register(RegisterViewModel model);
        UserViewModel Authenticate(string username, string password);
        BaseResponse ChangePassword(string email, string curPassword, string newPasswd);
    }
}