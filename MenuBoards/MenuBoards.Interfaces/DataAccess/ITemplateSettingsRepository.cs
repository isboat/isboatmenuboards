using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Interfaces.DataAccess
{
    public interface ITemplateSettingsRepository
    {
        SingleColumnSettings GetSingleColumnSettings(string settingsId);
    }
}