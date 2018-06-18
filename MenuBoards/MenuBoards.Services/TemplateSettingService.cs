using System.Collections.Generic;
using MenuBoards.DataAccess;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Services
{
    public class TemplateSettingService: ITemplateSettingService
    {
        private readonly ISettingsRepository repository;

        public TemplateSettingService()
        {
            this.repository = new SettingsRepository();
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