using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface ISlideRepository
    {
        string CreateMenuSlide(MenuSlide model);

        List<MenuSlide> GetAllMenuSlides();

        List<MenuSlide> GetAccountVisibleSlides(string account);

        BaseResponse DeleteSlide(string slideId);
    }
}