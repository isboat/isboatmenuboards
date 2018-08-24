using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Services;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideService slideService = IoC.Container.Resolve<ISlideService>();
        private readonly IMenuService menuService = IoC.Container.Resolve<IMenuService>();
        
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
                    if (!string.IsNullOrEmpty(slideId))
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

        public ActionResult SlideDetails(string id)
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

        public ActionResult DeleteSlide(string id)
        {
            try
            {
                var slide = this.slideService.DeleteSlide(id);
                return View(slide);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        

        public ActionResult MoveMenu(string id, string slideId, MoveDirection direction)
        {
            var response = this.menuService.MoveMenu(id, slideId, direction);
            return RedirectToAction("SlideDetails", new {id = slideId});
        }

        //[HttpGet]
        //public JsonResult SlideMenus(string slideId)
        //{
        //    var result = this.menuService.GetMenus(slideId);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult SaveMenu(Menu menu)
        //{
        //    if (!string.IsNullOrEmpty(menu?.Id))
        //    {
        //        var result = this.menuService.SaveMenu(menu);
        //        return Json(result, JsonRequestBehavior.DenyGet);
        //    }

        //    return Json(new BaseResponse {Message = "Menu or id is null"}, JsonRequestBehavior.DenyGet);
        //}

        //[HttpGet]
        //public JsonResult DeleteMenu(string id)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(id))
        //        {
        //            var result = this.menuService.DeleteMenu(id);
        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }

        //        return Json(new BaseResponse { Message = "id is null" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new BaseResponse { Message = "Error occured" }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpGet]
        public JsonResult GetDisplaySettings(string slideId)
        {
            try
            {
                var designs = this.slideService.GetDisplaySettings(slideId);
                return Json(designs, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
