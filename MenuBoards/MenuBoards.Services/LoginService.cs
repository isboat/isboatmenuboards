using System;
using System.Web;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class LoginService: ILoginService
    {
        private readonly ISessionService sessionService;

        private readonly IUserStateService userStateService;

        private readonly IAccountRepository accountRepository;

        public LoginService(ISessionService sessionService, IUserStateService userStateService, IAccountRepository accountRepository)
        {
            this.sessionService = sessionService;
            this.userStateService = userStateService;
            this.accountRepository = accountRepository;
        }

        public UserViewModel LogIn(string username, string password)
        {
            var authResponse = this.Authenticate(username, password);

            if (authResponse != null)
            {
                var sessionKey = Guid.NewGuid().ToString();

                this.sessionService.Set(sessionKey,
                    new UserSession
                    {
                        Username = username,
                        AccountId = authResponse.AccountId.ToString(),
                        FirstName = authResponse.FirstName,
                        LastName = authResponse.LastName
                    });

                HttpContext.Current.Response.Cookies.Add(new HttpCookie(Constants.COOKIE_KEY, sessionKey));
            }

            return authResponse;
        }

        private UserViewModel Authenticate(string username, string password)
        {
            return this.accountRepository.Authenticate(username, password);
        }

        public void LogOut()
        {
            if (this.userStateService.IsLoggedIn)
            {
                this.sessionService.Remove(this.userStateService.CurrentSessionKey);
                HttpContext.Current.Response.Cookies.Remove(Constants.COOKIE_KEY);
            }
        }
    }
}