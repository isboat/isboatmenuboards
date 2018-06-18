using System;
using System.Collections.Generic;
using System.Linq;
using MenuBoards.DataAccess;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Services
{
    public class SlideService: ISlideService
    {
        private readonly ISettingsRepository settingsRepository;

        private readonly IMenuSlideRepository menuSlideRepository;

        public SlideService()
        {
            this.settingsRepository = new SettingsRepository();
            this.menuSlideRepository = new MenuSlideRepository();
        }

        public string CreateMenuSlide(MenuSlide model)
        {
            return this.menuSlideRepository.CreateMenuSlide(model);
        }

        public List<Slide> GetAllSlides()
        {
            var slides = this.menuSlideRepository.GetAllMenuSlides()
                .Select(x => new Slide { Id = x.Id, Name = x.Name, SlideType = x.SlideType}).ToList();

            return slides;
        }

        public MenuSlide GetMenuSlideDetails(string id)
        {
            var all = this.menuSlideRepository.GetAllMenuSlides();
            var slide = all.FirstOrDefault(x => x.Id == id);
            return slide;
        }

        public BaseResponse DeleteSlide(string id)
        {
            return this.menuSlideRepository.DeleteSlide(id);
        }

        public DesignSettings GetDesignSettings(string slideId)
        {
            var design = this.settingsRepository.GetDesignSettings(slideId) ?? new DesignSettings();
            
            var templates = this.GetAllTemplates();

            foreach (var templateType in templates)
            {
                templateType.Selected = templateType.Id == design.TemplateType;
            }
            design.TemplateTypeOptions = templates;

            design.CurrencyOptions = UiHelper.GetCurrencyOptions(design.Currency);
            design.SubTemplates = this.GetAllSubDesignTemplates(design.SubTemplate);

            design.DefaultTemplateSettings = GetDefaultTemplateSettings();

            return design;
        }

        public DisplaySettings GetDisplaySettings(string slideId)
        {
            var displaySettings = this.settingsRepository.GetDisplaySettings(slideId) ?? new DisplaySettings();
            return displaySettings;
        }

        public BaseResponse SaveDesignSettings(DesignSettings settings)
        {
            settings.TemplateSettings.DesignId = settings.Id;
            var response = this.settingsRepository.SaveDesignSettings(settings);

            if (response.Success) this.SetDateTimeStamp(settings.SlideId);

            return response;
        }

        public BaseResponse SaveDisplaySettings(DisplaySettings settings)
        {
            var response = this.settingsRepository.SaveDisplaySettings(settings);
            if (response.Success) this.SetDateTimeStamp(settings.SlideId);
            return response;
        }

        public BaseResponse SaveMenu(Menu menu)
        {
            var response = this.menuSlideRepository.SaveMenu(menu);
            if (response.Success) this.SetDateTimeStamp(menu.SlideId);
            return response;
        }

        public List<Menu> GetSlideMenus(string slideId)
        {
            return this.menuSlideRepository.GetAllSlideMenus(slideId);
        }

        public BaseResponse DeleteMenu(string id)
        {
            var response = this.menuSlideRepository.DeleteMenu(id);

            if(response.Success) this.SetDateTimeStamp(response.SlideId);
            return response;
        }

        public List<SubTemplateSelectionItem> GetSubDesignTemplates(string parentId)
        {
            var designs = new List<SubTemplateSelectionItem>();

            return designs;
        }

        public string GetDateTimeStamp(string slideId)
        {
            return this.menuSlideRepository.GetDateTimeStamp(slideId);
        }

        private void SetDateTimeStamp(string slideId)
        {
            var ticks = DateTime.Now.Ticks.ToString();
            this.menuSlideRepository.SetDateTimeStamp(slideId, string.Format("dt{0}dt", ticks));
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

        private List<ITemplateSetting> GetDefaultTemplateSettings()
        {
            return new List<ITemplateSetting>
            {
                new SingleColumnSettings(),
                new TwoColumnsSettings(),
                new ThreeColumnsSettings()
            };
        }

        private List<MenuTemplateType> GetAllTemplates()
        {
            var list = new List<MenuTemplateType>
            {
                TemplateType.GetSingleColumn(),
                TemplateType.GetTwoColumn(),
                TemplateType.GetThreeColumn()
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