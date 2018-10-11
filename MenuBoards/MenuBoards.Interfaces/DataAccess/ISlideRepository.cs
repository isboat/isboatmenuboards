using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface ISlideRepository
    {
        string CreateMenuSlide(MenuSlide model);

        MenuSlide GetMenuSlide(string id);

        List<MenuSlide> GetAllMenuSlides(string accountId);

        List<MenuSlide> GetAccountVisibleSlides(string account);

        BaseResponse DeleteSlide(string slideId);
    }
}