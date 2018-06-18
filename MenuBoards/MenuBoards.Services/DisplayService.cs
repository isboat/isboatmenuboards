using System;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Services
{
    public class DisplayService: IDisplayService
    {
        private readonly ISlideService _slideService;
        
        public DisplayService(ISlideService slideService)
        {
            this._slideService = slideService;
        }
        public MenuSlideDisplay GetMenuSlide(string slideId)
        {
            var slide = this._slideService.GetMenuSlideDetails(slideId);

            if (slide == null)
            {
                return null;
            }

            var display = new MenuSlideDisplay
            {
                DateTimeStamp = this._slideService.GetDateTimeStamp(slide.Id),
                MenuSlide = slide,
                DesignSettings = this._slideService.GetDesignSettings(slideId),
                DisplaySettings = this._slideService.GetDisplaySettings(slideId)
            };

            display.MenuSlide.Menus = this._slideService.GetSlideMenus(slideId);

            SetLiveStatus(display);

            return display;
        }

        public string GetDateTimeStamp(string slideId)
        {
            return this._slideService.GetDateTimeStamp(slideId);
        }

        private void SetLiveStatus(MenuSlideDisplay display)
        {
            display.IsLive = DateTime.Parse(display.DisplaySettings.GoLiveDatetime) < DateTime.Now &&
                             !display.DisplaySettings.Disable;
        }
    }
}