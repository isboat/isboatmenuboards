using System;
using System.Collections.Generic;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Interfaces.Web
{
    public interface ISlideService
    {
        string CreateMenuSlide(MenuSlide model);

        List<Slide> GetAllSlides();

        MenuSlide GetSlideDetails(string id);

        BaseResponse DeleteSlide(string id);
        
        #region Display settings

        DisplaySettings GetDisplaySettings(string slideId);

        #endregion

        IEnumerable<Slide> GetAccountVisibleSlides(string account);
    }
}