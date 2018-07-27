using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Web.Controllers
{
    public class GlobalSettingsController : Controller
    {
        private readonly IGlobalSettingsService globalSettingsService = IoC.Container.Resolve<IGlobalSettingsService>();

        // GET: GlobalSettings
        public ActionResult Index()
        {
            GlobalSettings model = this.globalSettingsService.GetSettings("testaccount");
            return View(model);
        }
        
        public ActionResult CreateDisplayCode()
        {
            this.globalSettingsService.CreateDisplayCode("testaccount");
            return RedirectToAction("Index");
        }

        // GET: GlobalSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: GlobalSettings/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GlobalSettings/Edit/5
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

        // GET: GlobalSettings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GlobalSettings/Delete/5
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
