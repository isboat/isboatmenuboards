using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.DataAccess
{
    public class SettingsRepository: ISettingsRepository
    {
        private List<DesignSettings> designsettings = new List<DesignSettings>();

        private List<DisplaySettings> displaySettings = new List<DisplaySettings>();

        private List<ITemplateSetting> templateSettings = new List<ITemplateSetting>();

        public SingleColumnSettings GetSingleColumnSettings(string settingsId)
        {
            return new SingleColumnSettings
            {
                Id = "dffdfs",
                BackgroundColor = "black",
                HeadingBkgdColor = "white",
                HeadingColor = "brown",
                MenuItemSubTextSize = "12",
                MenuItemPriceTextSize = "12",
                HeadingTextSize = "14",
                MenuItemTextSize = "12",
                MenuItemColor = "red"
            };
        }

        public DesignSettings GetDesignSettings(string slideId)
        {
            var design = this.designsettings.FirstOrDefault(x => x.SlideId == slideId);


            if (design == null) return null;

            design.TemplateSettings = this.templateSettings.FirstOrDefault(x => x.DesignId == design.Id);

            return design;
        }

        public BaseResponse SaveDesignSettings(DesignSettings settings)
        {
            var existing = this.designsettings.FirstOrDefault(x => x.Id == settings.Id);
            if (existing != null)
            {
                this.designsettings.Remove(existing);
            }

            this.designsettings.Add(settings);

            var existingttmp = this.templateSettings.FirstOrDefault(x => x.DesignId == settings.TemplateSettings.DesignId);
            if (existingttmp != null)
            {
                this.templateSettings.Remove(existingttmp);
            }

            this.templateSettings.Add(settings.TemplateSettings);

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

                return new BaseResponse { Success = true };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BaseResponse(e.Message);
            }
        }
    }
}
