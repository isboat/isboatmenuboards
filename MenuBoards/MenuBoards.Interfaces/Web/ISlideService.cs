using System;
using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface ISlideService
    {
        int CreateMenuSlide(MenuSlide model);

        MenuSlide GetSlideDetails(int id);

        DesignSettings GetDesignSettings(string slideId);

        BaseResponse SaveMenu(Menu menu);

        BaseResponse DeleteMenu(string id);
    }
}