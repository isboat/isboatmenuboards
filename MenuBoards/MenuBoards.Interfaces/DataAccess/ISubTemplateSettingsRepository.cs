using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface ISubTemplateSettingsRepository
    {
        SingleColumnBasicSettings GetSingleColumnBasicSettings(string settingsId);
    }
}