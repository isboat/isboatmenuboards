using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface IMenuSlideRepository: IBaseSlideRepository
    {
        string CreateMenuSlide(MenuSlide model);

        List<MenuSlide> GetAllMenuSlides();
        BaseResponse DeleteSlide(string slideId);

        BaseResponse SaveMenu(Menu menu);

        List<Menu> GetAllSlideMenus(string slideId);

        DeleteResponse DeleteMenu(string menuId);
    }
}