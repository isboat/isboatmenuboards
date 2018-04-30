using System;
using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface ISlideService
    {
        int CreateMenuSlide(MenuSlide model);

        List<MenuSlide> GetAllSlides();

        MenuSlide GetSlideDetails(int id);

        BaseResponse DeleteSlide(int id);

        DesignSettings GetDesignSettings(string slideId);

        BaseResponse GetDesignSettings(SaveDesignSettings settings);

        BaseResponse SaveMenu(Menu menu);

        BaseResponse DeleteMenu(string id);
    }
}