using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.DataAccess
{
    public class SettingsRepository: ISettingsRepository
    {
        private readonly List<SlideDesignSettings> slidedesignsettings = new List<SlideDesignSettings>();

        private readonly List<DisplaySettings> displaySettings = new List<DisplaySettings>();

        private readonly Dictionary<string, string> displayCodes = new Dictionary<string, string>();

        private readonly ITimeStampRepository _timeStampRepository;

        public SettingsRepository(ITimeStampRepository timeStampRepository)
        {
            _timeStampRepository = timeStampRepository;
        }

        public SlideDesignSettings GetDesignSettings(string slideId)
        {
            var design = this.slidedesignsettings.FirstOrDefault(x => x.SlideId == slideId);

            return design;
        }

        public BaseResponse SaveDesignSettings(SlideDesignSettings settings)
        {
            var existing = this.slidedesignsettings.FirstOrDefault(x => x.Id == settings.Id);
            if (existing != null)
            {
                this.slidedesignsettings.Remove(existing);
            }

            this.slidedesignsettings.Add(settings);

            this._timeStampRepository.UpdateTimeStamp(settings.SlideId);
            return new BaseResponse {Success = true};
        }

        public DisplaySettings GetDisplaySettings(string slideId)
        {
            try
            {
                return this.displaySettings.FirstOrDefault(x => x.SlideId == slideId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public BaseResponse SaveDisplaySettings(DisplaySettings settings)
        {
            try
            {
                var existing = this.displaySettings.FirstOrDefault(x => x.Id == settings.Id);
                if (existing != null)
                {
                    this.displaySettings.Remove(existing);
                }

                this.displaySettings.Add(settings);

                this._timeStampRepository.UpdateTimeStamp(settings.SlideId);
                return new BaseResponse { Success = true };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse(e.Message);
            }
        }

        public DisplayCodeResponse VerifyDisplayCode(DisplayCode code)
        {
            var response = new DisplayCodeResponse();
            if (!this.displayCodes.ContainsKey(code.Code))
            {
                response.Message = "Code doesn't exist";
                return response;
            }

            response.Success = true;
            response.Account = this.displayCodes[code.Code];
            return response;
        }
    }
}
