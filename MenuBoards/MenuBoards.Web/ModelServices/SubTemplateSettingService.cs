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
    public class SubTemplateSettingService: ISubTemplateSettingService
    {
        private readonly ISubTemplateSettingsRepository repository;

        public SubTemplateSettingService()
        {
            this.repository = new SubTemplateSettingsRepository();
        }

        public ISubTemplateSetting GetSubTemplateSettings(string settingId, string subTemplateType, string templateType)
        {
            if (templateType == "1") // SingleCol
            {
                return this.GetSingleColumnSettings(settingId, subTemplateType);
            }

            return null;
        }

        public List<ISubTemplateSetting> GetDefaultSubTemplateSettings()
        {
            var list = new List<ISubTemplateSetting>
            {
                new SingleColumnBasicSettings(),
                new SingleColumnBronzeSettings(),
                new SingleColumnSilverSettings()
            };

            return list;
        }

        private ISubTemplateSetting GetSingleColumnSettings(string settingId, string subTemplateType)
        {
            if (subTemplateType == "1")
            {
                return repository.GetSingleColumnBasicSettings(settingId);
            }

            if (subTemplateType == "2")
            {
                return this.GetSingleColumnBronzeSettings(settingId);
            }

            return null;
        }

        private SingleColumnBronzeSettings GetSingleColumnBronzeSettings(string settingId)
        {
            return new SingleColumnBronzeSettings();
        }
    }
}