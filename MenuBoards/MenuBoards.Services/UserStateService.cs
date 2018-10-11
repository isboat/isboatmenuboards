using System.Web;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class UserStateService: IUserStateService
    {
        #region Instance variables

        /// <summary>
        /// The session service
        /// </summary>
        private readonly ISessionService sessionService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStateService"/> class.
        /// </summary>
        /// <param name="sessionService">The session service.</param>
        public UserStateService(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        #endregion

        /// <summary>
        /// Gets or sets the current session key.
        /// </summary>
        /// <value>
        /// The current session key.
        /// </value>
        public string CurrentSessionKey
            => HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY] != null
                ? HttpContext.Current.Request.Cookies[Constants.COOKIE_KEY].Value
                : string.Empty;

        /// <summary>
        /// Gets the who is logged in.
        /// </summary>
        /// <value>
        /// The who is logged in.
        /// </value>
        public UserSession WhoIsLoggedIn
        {
            get
            {
                if (!string.IsNullOrEmpty(this.CurrentSessionKey))
                {
                    var session = this.sessionService.Get<UserSession>(this.CurrentSessionKey);

                    if (session != null)
                    {
                        return session;
                    }
                }

                return new UserSession();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is logged in.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedIn
        {
            get
            {
                if (!string.IsNullOrEmpty(this.CurrentSessionKey))
                {
                    var session = this.sessionService.Get<UserSession>(this.CurrentSessionKey);

                    if (session != null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}