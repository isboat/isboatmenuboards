using System;
using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface ISlideService
    {
        string CreateMenuSlide(MenuSlide model);

        List<Slide> GetAllSlides();

        MenuSlide GetMenuSlideDetails(string id);

        BaseResponse DeleteSlide(string id);

        DesignSettings GetDesignSettings(string slideId);

        BaseResponse SaveDesignSettings(DesignSettings settings);

        #region Display settings

        DisplaySettings GetDisplaySettings(string slideId);

        BaseResponse SaveDisplaySettings(DisplaySettings settings);

        #endregion

        #region Menus

        BaseResponse SaveMenu(Menu menu);

        List<Menu> GetSlideMenus(string slideId);

        BaseResponse DeleteMenu(string id);

        #endregion

        #region Date time stamp

        string GetDateTimeStamp(string slideId);

        #endregion
    }
}