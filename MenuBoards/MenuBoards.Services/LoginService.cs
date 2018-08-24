using System;
using System.Web;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class LoginService: ILoginService
    {
        private readonly ISessionService sessionService;

        private readonly IUserStateService userStateService;

        public LoginService(ISessionService sessionService, IUserStateService userStateService)
        {
            this.sessionService = sessionService;
            this.userStateService = userStateService;
        }

        public BaseResponse LogIn(string username, string password)
        {
            var authResponse = this.Authenticate(username, password);

            if (authResponse.Success)
            {
                var sessionKey = Guid.NewGuid().ToString();

                this.sessionService.Set(sessionKey, new UserSession { Username = username });
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(Constants.COOKIE_KEY, sessionKey));
            }

            return authResponse;
        }

        private BaseResponse Authenticate(string username, string password)
        {
            return new BaseResponse { Success = true};
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

    /// <summary>
    /// User Session object.
    /// </summary>
    internal class UserSession
    {
        public string Username;
    }
}