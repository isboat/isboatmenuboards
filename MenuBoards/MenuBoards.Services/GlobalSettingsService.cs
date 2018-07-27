using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class GlobalSettingsService: IGlobalSettingsService
    {
        private readonly IGlobalSettingsRepository globalSettingsRepository;

        public GlobalSettingsService(IGlobalSettingsRepository globalSettingsRepository)
        {
            this.globalSettingsRepository = globalSettingsRepository;
        }

        public void CreateDisplayCode(string account)
        {
            this.globalSettingsRepository.CreateDisplayCode(account);
        }

        public GlobalSettings GetSettings(string account)
        {
            var settings = new GlobalSettings
            {
                DisplayCode = this.globalSettingsRepository.GetDisplayCode(account)
            };

            return settings;
        }
    }
}