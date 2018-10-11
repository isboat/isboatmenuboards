using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService loginService = IoC.Container.Resolve<ILoginService>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel model)
        {
            try
            {
                if (!ValidateRequest || !ModelState.IsValid) return View(model);

                var result = this.loginService.LogIn(model.Username, model.Password);

                if (result != null)
                {
                    return this.RedirectToAction("Index", "Dashboard");
                }

                model.Message = "Auth failed. Try again.";
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
        
        [HttpPost]
        public ActionResult LogOut()
        {
            this.loginService.LogOut();

            return this.RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}