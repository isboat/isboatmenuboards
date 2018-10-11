using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface IMenuRepository
    {
        List<Menu> GetMenus(string slideId);

        BaseResponse SaveMenu(Menu menu);

        DeleteResponse DeleteMenu(DeleteItem item);

        Menu GetMenu(string id);

        BaseResponse MoveMenu(string id, string slideId, MoveDirection direction);
    }
}