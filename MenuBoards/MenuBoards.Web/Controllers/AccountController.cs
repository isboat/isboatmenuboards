using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Services;
using MenuBoards.Web.Mvc;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Instances Variables

        private readonly IAccountService accountService = IoC.Container.Resolve<IAccountService>();

        #endregion

        //
        // POST: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registerauser()
        {
            return View("Register", new RegisterViewModel());
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registerauser(RegisterViewModel model)
        {
            if (this.ValidateRequest && this.ModelState.IsValid)
            {
                var baseResponse = this.accountService.Register(model);

                return View("BaseResponse", baseResponse);
            }

            return View("Register", model);
        }

        [RequiresAuthentication]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        [RequiresAuthentication]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ValidateRequest && this.ModelState.IsValid)
            {
                var baseResponse =
                    this.accountService.ChangePassword(model.Email, model.CurrentPassword, model.NewPassword);

                return View("BaseResponse", baseResponse);
            }

            return View(model);
        }
    }
}
