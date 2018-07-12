using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class DesignSettingsService: IDesignSettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        private readonly ITimeStampRepository _timeStampRepository;

        public DesignSettingsService(ISettingsRepository settingsRepository, ITimeStampRepository timeStampRepository)
        {
            _settingsRepository = settingsRepository;
            _timeStampRepository = timeStampRepository;
        }

        public SlideDesignSettings GetDesignSettings(string slideId)
        {
            var settings = this._settingsRepository.GetDesignSettings(slideId) ?? new SlideDesignSettings();

            return settings;
        }

        public BaseResponse SaveDesignSettings(SlideDesignSettings settings)
        {
            return this._settingsRepository.SaveDesignSettings(settings);
        }
    }
}