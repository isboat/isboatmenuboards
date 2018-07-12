using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface IMenuService
    {
        BaseResponse SaveMenu(Menu menu);

        List<Menu> GetMenus(string slideId);

        DeleteResponse DeleteMenu(string id);

        Menu GetMenu(string id);

        Menu GetMenuDetails(string id);
    }
}