using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ModelServices;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideService slideService = new SlideService();

        private readonly ITemplateSettingService subTemplateSettingService = new TemplateSettingService();
        
        // GET: Slide/Create
        public ActionResult CreateMenuSlide()
        {
            var model = new MenuSlide();

            return View(model);
        }

        // POST: Slide/Create
        [HttpPost]
        public ActionResult CreateMenuSlide(MenuSlide model)
        {
            try
            {
                if (model != null && !string.IsNullOrEmpty(model.Name))
                {
                    var slideId = this.slideService.CreateMenuSlide(model);
                    if (slideId > 0)
                    {
                        return RedirectToAction("SlideDetails", new {id = slideId});
                    }
                }
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewAllSlides()
        {
            try
            {
                var slides = this.slideService.GetAllSlides();
                return View(slides);
            }
            catch
            {
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult SlideDetails(int id)
        {
            try
            {
                var slide = this.slideService.GetSlideDetails(id);
                return View(slide);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteSlide(int id)
        {
            try
            {
                var slide = this.slideService.GetSlideDetails(id);
                return View(slide);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        
        [HttpGet]
        public JsonResult GetDefaultSubTemplateSettings()
        {
            var settings = this.subTemplateSettingService.GetDefaultSubTemplateSettings();
            return Json(settings, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult SaveMenu(Menu menu)
        {
            if (!string.IsNullOrEmpty(menu?.Id))
            {
                var result = this.slideService.SaveMenu(menu);
                return Json(result, JsonRequestBehavior.DenyGet);
            }

            return Json(new BaseResponse {Message = "Menu or id is null"}, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public JsonResult DeleteMenu(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var result = this.slideService.DeleteMenu(id);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                return Json(new BaseResponse { Message = "id is null" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new BaseResponse { Message = "Error occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetDesignSettings(string slideId)
        {
            try
            {
                var designs = this.slideService.GetDesignSettings(slideId);
                return Json(designs, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveDesignSettings(SaveDesignSettings settings)
        {
            try
            {
                var response = this.slideService.GetDesignSettings(settings);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveSubTemplateSettings(SaveDesignSettings settings)
        {
            try
            {
                var response = this.slideService.GetDesignSettings(settings);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
