using System.Collections.Generic;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Interfaces.Web
{
    public interface ITemplateSettingService
    {
        ITemplateSetting GetTemplateSettings(string settingId, string templateType);
        List<ITemplateSetting> GetDefaultSubTemplateSettings();
    }
}