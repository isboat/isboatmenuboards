using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public BaseResponse Register(RegisterViewModel model)
        {
            return this.accountRepository.Register(model);
        }

        public BaseResponse ChangePassword(string email, string curPassword, string newPasswd)
        {
            return this.accountRepository.ChangePassword(email, curPassword, newPasswd);
        }
    }
}