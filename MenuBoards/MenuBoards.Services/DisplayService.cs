using System;
using System.Collections.Generic;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Services
{
    public class DisplayService: IDisplayService
    {
        private readonly ISlideService _slideService;

        private readonly IMenuService _menuService;

        private readonly IDesignSettingsService _designSettingsService;

        private readonly IDisplaySettingsService _displaySettingsService;

        private readonly ITimeStampRepository _timeStampRepository;

        private readonly IGlobalSettingsRepository _globalSettingsRepository;

        public DisplayService(ISlideService slideService, 
            IMenuService menuService, 
            IDesignSettingsService designSettingsService, 
            IDisplaySettingsService displaySettingsService, 
            ITimeStampRepository timeStampRepository, IGlobalSettingsRepository globalSettingsRepository)
        {
            this._slideService = slideService;
            this._menuService = menuService;
            _designSettingsService = designSettingsService;
            _displaySettingsService = displaySettingsService;
            _timeStampRepository = timeStampRepository;
            _globalSettingsRepository = globalSettingsRepository;
        }
        public MenuSlideDisplay GetMenuSlide(string slideId, bool previewMode)
        {
            var slide = this._slideService.GetSlideDetails(slideId);

            if (slide == null)
            {
                return null;
            }

            var display = new MenuSlideDisplay
            {
                DateTimeStamp = this._timeStampRepository.GetTimeStamp(slide.Id),
                MenuSlide = slide,
                DesignSettings = this._designSettingsService.GetDesignSettings(slideId),
                DisplaySettings = this._displaySettingsService.GetDisplaySettings(slideId)
            };

            display.MenuSlide.Menus = this._menuService.GetMenus(slideId);

            if (previewMode)
            {
                display.IsLive = true;
            }
            else
            {
                SetLiveStatus(display);
            }

            return display;
        }

        public DisplayCodeResponse VerifyDisplayCode(DisplayCode code)
        {
            return this._displaySettingsService.VerifyDisplayCode(code);
        }

        public string GetTimeStamp(string slideId)
        {
            return this._timeStampRepository.GetTimeStamp(slideId);
        }

        public IEnumerable<Slide> LoadVisibleSlides(string account)
        {
            return this._slideService.GetAccountVisibleSlides(account);
        }

        public bool IsDisplayCodeChange(string account)
        {
            return this._globalSettingsRepository.IsDisplayCodeChange(account);
        }

        private void SetLiveStatus(MenuSlideDisplay slide)
        {
            slide.IsLive = !string.IsNullOrEmpty(slide.DisplaySettings.GoLiveDatetime) 
                && DateTime.Parse(slide.DisplaySettings.GoLiveDatetime) < DateTime.Now 
                && !slide.DisplaySettings.Disable;
        }
    }
}