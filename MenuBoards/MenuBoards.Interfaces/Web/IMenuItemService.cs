using System.Collections.Generic;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Images;

namespace MenuBoards.Interfaces.Web
{
    public interface IMenuItemService
    {
        List<MenuItem> GetMenuItems(string menuId);

        BaseResponse SaveMenuItem(MenuItem item);

        DeleteResponse DeleteMenuItem(DeleteItem item);

        MenuItem GetItem(string id);
        BaseResponse UpdateImageUrl(AddImage image);
    }
}