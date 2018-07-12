using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Services
{
    public class DisplaySettingsService : IDisplaySettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public DisplaySettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public DisplaySettings GetDisplaySettings(string slideId)
        {
            var settings = this._settingsRepository.GetDisplaySettings(slideId) ?? new DisplaySettings();

            return settings;
        }

        public BaseResponse SaveDisplaySettings(DisplaySettings settings)
        {
            return this._settingsRepository.SaveDisplaySettings(settings);
        }

        public DisplayCodeResponse VerifyDisplayCode(DisplayCode code)
        {
            return this._settingsRepository.VerifyDisplayCode(code);
        }
    }
}