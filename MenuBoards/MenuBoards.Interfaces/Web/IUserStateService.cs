using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface IUserStateService
    {
        /// <summary>
        /// Gets the current session key.
        /// </summary>
        /// <value>
        /// The current session key.
        /// </value>
        string CurrentSessionKey { get; }

        /// <summary>
        /// Gets the who is logged in.
        /// </summary>
        /// <value>
        /// The who is logged in.
        /// </value>
        UserSession WhoIsLoggedIn { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logged in; otherwise, <c>false</c>.
        /// </value>
        bool IsLoggedIn { get; }
    }
}