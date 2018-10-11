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
using MenuBoards.Web.Mvc;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideService slideService = IoC.Container.Resolve<ISlideService>();
        private readonly IMenuService menuService = IoC.Container.Resolve<IMenuService>();

        
        [RequiresAuthentication]
        public ActionResult CreateMenuSlide()
        {
            var model = new MenuSlide();

            return View(model);
        }

        
        [HttpPost]
        [RequiresAuthentication]
        public ActionResult CreateMenuSlide(MenuSlide model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model?.Name))
                {
                    var slideId = this.slideService.CreateMenuSlide(model);
                    if (!string.IsNullOrEmpty(slideId))
                    {
                        return RedirectToAction("SlideDetails", new {id = slideId});
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [RequiresAuthentication]
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

        [RequiresAuthentication]
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

        [RequiresAuthentication]
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

        [RequiresAuthentication]
        public ActionResult MoveMenu(string id, string slideId, MoveDirection direction)
        {
            var response = this.menuService.MoveMenu(id, slideId, direction);
            return RedirectToAction("SlideDetails", new {id = slideId});
        }

        [RequiresAuthentication]
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
