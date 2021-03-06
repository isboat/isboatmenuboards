﻿using System;
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

        public ActionResult EnterCode()
        {
            return View(new DisplayCode());
        }

        [HttpPost]
        public ActionResult EnterCode(DisplayCode modelDisplayCode)
        {
            if (ValidateRequest && modelDisplayCode != null)
            {
                var response = this.displayService.VerifyDisplayCode(modelDisplayCode);
                if (response.Success)
                {
                    var cookie = new HttpCookie(Constants.VERIFIED_COOKIE_NAME, "true");
                    this.Response.Cookies.Add(cookie);
                    IEnumerable<Slide> slides = this.displayService.LoadVisibleSlides(response.Account);
                    return View("VisibleSlides", slides);
                }

                modelDisplayCode.ResponseMsg = response.Message;
            }
            return View(modelDisplayCode);
        }

        public ActionResult MenuSlide(string slideId)
        {
            var verifiedCookie = this.Request.Cookies[Constants.VERIFIED_COOKIE_NAME];
            if (verifiedCookie == null || verifiedCookie.Value != "true")
            {
                return RedirectToAction("EnterCode");
            }

            MenuSlideDisplay slide = null;

            if (!string.IsNullOrEmpty(slideId))
            {
                slide = this.displayService.GetMenuSlide(slideId, false);
            }
            return View(slide);
        }

        public ActionResult PreviewMenuSlide(string slideId)
        {
            MenuSlideDisplay slide = null;

            if (!string.IsNullOrEmpty(slideId))
            {
                slide = this.displayService.GetMenuSlide(slideId, true);
            }
            return View("MenuSlide", slide);
        }


        [HttpGet]
        public JsonResult Ping(string slideId, string dt, string account, string displayCode)
        {
            var response = this.displayService.VerifyDisplayCode(new DisplayCode { Account = account, Code = displayCode});
            if (!response.Success)
            {
                var cookie = new HttpCookie(Constants.VERIFIED_COOKIE_NAME, "false") {Expires = DateTime.Now.AddDays(-1d)};
                this.Response.Cookies.Add(cookie);

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            var hasNewData = false;

            if (!string.IsNullOrEmpty(slideId) && !string.IsNullOrEmpty(dt))
            {
                hasNewData = this.displayService.GetTimeStamp(slideId) != dt;
            }
            return Json(hasNewData, JsonRequestBehavior.AllowGet);
        }
    }
}
