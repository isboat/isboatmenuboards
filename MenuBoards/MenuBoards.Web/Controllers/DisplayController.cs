using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Services;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Web.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IDisplayService displayService = IoC.Container.Resolve<IDisplayService>();

        private const string VERIFIED_COOKIE_NAME = "verifiedcode";

        public ActionResult DisplayCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DisplayCode(DisplayCode code)
        {
            if (ValidateRequest && code != null)
            {
                var response = this.displayService.VerifyDisplayCode(code);
                if (response.Success)
                {
                    var cookie = new HttpCookie(VERIFIED_COOKIE_NAME, "true");
                    this.Response.Cookies.Add(cookie);
                    IEnumerable<Slide> slides = this.displayService.LoadVisibleSlides(response.Account);
                    return View("VisibleSlides", slides);
                }
            }
            return View();
        }

        public ActionResult MenuSlide(string slideId, bool previewMode)
        {
            var verifiedCookie = this.Request.Cookies[VERIFIED_COOKIE_NAME];
            if (verifiedCookie == null || verifiedCookie.Value != "true")
            {
                return RedirectToAction("DisplayCode");
            }

            MenuSlideDisplay slide = null;

            if (!string.IsNullOrEmpty(slideId))
            {
                slide = this.displayService.GetMenuSlide(slideId, previewMode);
            }
            return View(slide);
        }


        [HttpGet]
        public JsonResult Ping(string slideId, string dt)
        {
            var hasNewData = false;

            if (!string.IsNullOrEmpty(slideId) && !string.IsNullOrEmpty(dt))
            {
                hasNewData = this.displayService.GetTimeStamp(slideId) != dt;
            }
            return Json(hasNewData, JsonRequestBehavior.AllowGet);
        }
    }
}
