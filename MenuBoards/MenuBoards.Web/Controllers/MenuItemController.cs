using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Images;

namespace MenuBoards.Web.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IMenuItemService menuItemService = IoC.Container.Resolve<IMenuItemService>();

        public ActionResult AddItem(string menuId, string slideId)
        {
            var menu = new MenuItem { MenuId = menuId, SlideId = slideId};
            return View(menu);
        }

        [HttpPost]
        public ActionResult AddItem(MenuItem item)
        {
            if (ValidateRequest && ModelState.IsValid)
            {
                var response = this.menuItemService.SaveMenuItem(item);
                return RedirectToAction("Details", "Menu", new { id = item.MenuId });
            }
            else
            {
                return View(item);
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(string id)
        {
            var menu = this.menuItemService.GetItem(id);
            return View("AddItem", menu);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(MenuItem item)
        {
            if (ValidateRequest && ModelState.IsValid)
            {
                var response = this.menuItemService.SaveMenuItem(item);
                return RedirectToAction("Details", "Menu", new { id = item.MenuId });
            }
            else
            {
                return View("AddItem", item);
            }
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(string id, string slideId)
        {
            var response = this.menuItemService.DeleteMenuItem(new DeleteItem { Id = id, SlideId = slideId});
            return RedirectToAction("Details", "Menu", new { id = response.ItemId });
        }

        public ActionResult AddImage(string id, string slideId, string returnUrl)
        {
            return View(new AddImage { ItemId = id, ReturnUrl = returnUrl});
        }

        [HttpPost]
        public ActionResult AddImage(AddImage image)
        {
            if (ValidateRequest)
            {
                var response = this.menuItemService.UpdateImageUrl(image);
                return Redirect(image.ReturnUrl);
            }

            return View(image);
        }
    }
}
