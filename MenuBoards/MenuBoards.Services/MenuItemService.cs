using System;
using System.Collections.Generic;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Images;

namespace MenuBoards.Services
{
    public class MenuItemService: IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }
        public List<MenuItem> GetMenuItems(string menuId)
        {
            return this._menuItemRepository.GetMenuItems(menuId);
        }

        public BaseResponse SaveMenuItem(MenuItem item)
        {
            if (string.IsNullOrEmpty(item.Id))
            {
                item.Id = Guid.NewGuid().ToString().Replace("-", "");
            }

            return this._menuItemRepository.SaveMenuItem(item);
        }

        public DeleteResponse DeleteMenuItem(DeleteItem item)
        {
            return this._menuItemRepository.DeleteMenuItem(item);
        }

        public MenuItem GetItem(string id)
        {
            return this._menuItemRepository.GetItem(id);
        }

        public BaseResponse UpdateImageUrl(AddImage image)
        {
            return this._menuItemRepository.UpdateImageUrl(image);
        }
    }
}