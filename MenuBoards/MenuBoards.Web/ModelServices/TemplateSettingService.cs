using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MenuBoards.DataAccess;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Web.ModelServices
{
    public class TemplateSettingService: ITemplateSettingService
    {
        private readonly ITemplateSettingsRepository repository;

        public TemplateSettingService()
        {
            this.repository = new SubTemplateSettingsRepository();
        }

        public ITemplateSetting GetTemplateSettings(string settingId, string templateType)
        {
            if (templateType == "1") // SingleCol
            {
                return this.GetSingleColumnSettings(settingId);
            }

            return null;
        }

        public List<ITemplateSetting> GetDefaultSubTemplateSettings()
        {
            var list = new List<ITemplateSetting>
            {
                new SingleColumnBasicSettings(),
                new SingleColumnBronzeSettings(),
                new SingleColumnSettings()
            };

            return list;
        }

        private ITemplateSetting GetSingleColumnSettings(string settingId)
        {
            return repository.GetSingleColumnSettings(settingId);
        }
    }
}