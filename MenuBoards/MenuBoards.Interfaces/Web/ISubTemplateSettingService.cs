using System.Collections.Generic;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Interfaces.Web
{
    public interface ISubTemplateSettingService
    {
        ISubTemplateSetting GetSubTemplateSettings(string settingId, string subTemplateType, string templateType);
        List<ISubTemplateSetting> GetDefaultSubTemplateSettings();
    }
}