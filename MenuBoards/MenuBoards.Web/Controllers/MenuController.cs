using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Services;
using MenuBoards.Web.Mvc;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService = IoC.Container.Resolve<IMenuService>();

        [RequiresAuthentication]
        public ActionResult AddMenu(string slideId)
        {
            var menu = new Menu {SlideId = slideId};
            return View(menu);
        }

        [RequiresAuthentication]
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

        [RequiresAuthentication]
        // GET: Menu/Edit/5
        public ActionResult Edit(string id)
        {
            var menu = this._menuService.GetMenu(id);
            return View("AddMenu", menu);
        }

        [RequiresAuthentication]
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

        [RequiresAuthentication]
        public ActionResult Details(string id)
        {
            var details = this._menuService.GetMenuDetails(id);
            return View(details);
        }

        [RequiresAuthentication]
        // GET: Menu/Delete/5
        public ActionResult Delete(string id, string slideId)
        {
            var response = this._menuService.DeleteMenu(new DeleteItem { Id = id, SlideId = slideId});
            return RedirectToAction("SlideDetails", "Slide", new { id = response.ItemId });
        }
    }
}
