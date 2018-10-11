using System;
using System.Collections.Generic;
using System.Linq;
using MenuBoards.DataAccess;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class SlideService: ISlideService
    {
        private readonly ISettingsRepository _settingsRepository;

        private readonly ISlideRepository _menuSlideRepository;

        private readonly IMenuRepository _menuRepository;

        private readonly IUserStateService stateService;

        public SlideService(
            ISettingsRepository settingsRepository, 
            ISlideRepository menuSlideRepository, 
            IMenuRepository menuRepository, 
            IUserStateService stateService)
        {
            this._settingsRepository = settingsRepository;
            this._menuSlideRepository = menuSlideRepository;
            this._menuRepository = menuRepository;
            this.stateService = stateService;
        }

        public string CreateMenuSlide(MenuSlide model)
        {
            model.AccountId = this.stateService.WhoIsLoggedIn.AccountId;
            return this._menuSlideRepository.CreateMenuSlide(model);
        }

        public List<Slide> GetAllSlides()
        {
            var slides = this._menuSlideRepository.GetAllMenuSlides(this.stateService.WhoIsLoggedIn.AccountId)
                .Select(x => new Slide { Id = x.Id, Name = x.Name, SlideType = x.SlideType}).ToList();

            foreach (var slide in slides)
            {
                slide.GoLiveDateTime = this._settingsRepository.GetDisplaySettings(slide.Id)?.GoLiveDatetime;
            }
            return slides;
        }

        public MenuSlide GetSlideDetails(string id)
        {
            var slide = this._menuSlideRepository.GetMenuSlide(id);

            if (slide != null)
            {
                slide.Menus = this._menuRepository.GetMenus(id).OrderBy(x => x.Position).ToList();
            }

            return slide;
        }

        public BaseResponse DeleteSlide(string id)
        {
            return this._menuSlideRepository.DeleteSlide(id);
        }

        public DisplaySettings GetDisplaySettings(string slideId)
        {
            var displaySettings = this._settingsRepository.GetDisplaySettings(slideId) ?? new DisplaySettings();
            return displaySettings;
        }

        public IEnumerable<Slide> GetAccountVisibleSlides(string account)
        {
            return this._menuSlideRepository.GetAccountVisibleSlides(account).Select(x => new Slide
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public List<SubTemplateSelectionItem> GetSubDesignTemplates(string parentId)
        {
            var designs = new List<SubTemplateSelectionItem>();

            return designs;
        }
        
        private List<SubTemplateSelectionItem> GetAllSubDesignTemplates(string selected)
        {
            var list = this.GetAllSubDesignTemplates();
            foreach (var design in list)
            {
                design.Selected = design.Id == selected;
            }

            return list;
        }
        

        private List<MenuTemplateType> GetAllTemplates()
        {
            var list = new List<MenuTemplateType>
            {
                TemplateTypes.GetSingleColumn(),
                TemplateTypes.GetTwoColumn(),
                TemplateTypes.GetThreeColumn()
            };

            return list;
        }
        
        private List<SubTemplateSelectionItem> GetAllSubDesignTemplates()
        {
            var all = new List<SubTemplateSelectionItem>();

            all.Add(new SubTemplateSelectionItem { Id = "SCBasic", Name = "Single column - Basic", ParentTemplateId = "1"});
            all.Add(new SubTemplateSelectionItem { Id = "SCBronze", Name = "Single column - Bronze", ParentTemplateId = "1"});
            all.Add(new SubTemplateSelectionItem { Id = "SCSilver", Name = "Single column - Silver", ParentTemplateId = "1"});

            all.Add(new SubTemplateSelectionItem { Id = "4", Name = "Two column 1", ParentTemplateId = "2" });
            all.Add(new SubTemplateSelectionItem { Id = "5", Name = "Two column 2", ParentTemplateId = "2"});
            all.Add(new SubTemplateSelectionItem { Id = "6", Name = "Two column 3", ParentTemplateId = "2" });

            all.Add(new SubTemplateSelectionItem { Id = "7", Name = "Three column 1", ParentTemplateId = "3" });
            all.Add(new SubTemplateSelectionItem { Id = "8", Name = "Three column 2", ParentTemplateId = "3"});
            all.Add(new SubTemplateSelectionItem { Id = "9", Name = "Three column 3", ParentTemplateId = "3" });

            return all;
        }
    }
}