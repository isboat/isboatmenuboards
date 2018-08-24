using System;
using System.Collections.Generic;
using MenuBoards.DataAccess;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class MenuService: IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        private readonly IMenuItemRepository _menuItemRepository;

        public MenuService(IMenuRepository menuRepository, IMenuItemRepository menuItemRepository)
        {
            this._menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
        }

        public BaseResponse SaveMenu(Menu menu)
        {
            return this._menuRepository.SaveMenu(menu);
        }

        public List<Menu> GetMenus(string slideId)
        {
            return this._menuRepository.GetMenus(slideId);
        }

        public DeleteResponse DeleteMenu(string id)
        {
            var response = this._menuRepository.DeleteMenu(id);

            if (response.Success)
            {
                var ticks = DateTime.Now.Ticks.ToString();
                //this._menuRepository.SetDateTimeStamp(slideId, string.Format("dt{0}dt", ticks));
            }
            return response;
        }

        public Menu GetMenu(string id)
        {
            return this._menuRepository.GetMenu(id);
        }

        public Menu GetMenuDetails(string id)
        {
            var menu = this._menuRepository.GetMenu(id);
            if (menu != null)
            {
                menu.MenuItems = this._menuItemRepository.GetMenuItems(id);
            }

            return menu;
        }

        public BaseResponse MoveMenu(string id, string slideId, MoveDirection direction)
        {
            return this._menuRepository.MoveMenu(id, slideId, direction);
        }
    }
}