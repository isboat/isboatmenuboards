using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;

namespace MenuBoards.Web.Mvc
{
    public class RequiresAuthenticationAttribute : ActionFilterAttribute, IResultFilter
    {
        void IResultFilter.OnResultExecuting(ResultExecutingContext resultExecutingContext)
        {
            var stateService = IoC.Container.Resolve<IUserStateService>();

            if (!stateService.IsLoggedIn)
            {
                resultExecutingContext.HttpContext.Response.Redirect("/home/login");
            }
        }
    }
}