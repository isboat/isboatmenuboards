using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Interfaces.Web;
using MenuBoards.Services;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Web.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IDisplayService displayService = DisplayIoC.Instance;

        // GET: Display/MenuSlide/5
        public ActionResult MenuSlide(string slideId)
        {
            MenuSlideDisplay slide = null;

            if (!string.IsNullOrEmpty(slideId))
            {
                slide = this.displayService.GetMenuSlide(slideId);
            }
            return View(slide);
        }

        // GET: Display/MenuSlide/5
        [HttpGet]
        public JsonResult HasNewData(string slideId, string dt)
        {
            var hasNewData = false;

            if (!string.IsNullOrEmpty(slideId) && !string.IsNullOrEmpty(dt))
            {
                hasNewData = this.displayService.GetDateTimeStamp(slideId) != dt;
            }
            return Json(hasNewData, JsonRequestBehavior.AllowGet);
        }

        // GET: Display/Group/5
        public ActionResult Group()
        {
            return View();
        }

        // GET: Content/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Content/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Content/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Content/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
