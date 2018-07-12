using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Services;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService = IoC.Container.Resolve<IMenuService>();
        
        public ActionResult AddMenu(string slideId)
        {
            var menu = new Menu {SlideId = slideId};
            return View(menu);
        }

        [HttpPost]
        public ActionResult AddMenu(Menu menu)
        {
            if (ValidateRequest && ModelState.IsValid)
            {
                var response = this._menuService.SaveMenu(menu);
                return RedirectToAction("SlideDetails", "Slide", new { id = menu.SlideId});
            }
            else
            {
                return View(menu);
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(string id)
        {
            var menu = this._menuService.GetMenu(id);
            return View("AddMenu", menu);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ValidateRequest && ModelState.IsValid)
            {
                var response = this._menuService.SaveMenu(menu);
                return RedirectToAction("SlideDetails", "Slide", new { id = menu.SlideId });
            }
            else
            {
                return View("AddMenu", menu);
            }
        }
        
        public ActionResult Details(string id)
        {
            var details = this._menuService.GetMenuDetails(id);
            return View(details);
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(string id)
        {
            var response = this._menuService.DeleteMenu(id);
            return RedirectToAction("SlideDetails", "Slide", new { id = response.ItemId });
        }
    }
}
