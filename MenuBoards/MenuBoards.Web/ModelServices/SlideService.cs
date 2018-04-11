using System;
using System.Collections.Generic;
using MenuBoards.Interfaces.Web;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Web.ModelServices
{
    public class SlideService: ISlideService
    {
        private readonly ISubTemplateSettingService settingService;

        public SlideService()
        {
            this.settingService = new SubTemplateSettingService();
        }

        public int CreateMenuSlide(MenuSlide model)
        {
            return 1;
        }

        public MenuSlide GetSlideDetails(int id)
        {
            return new MenuSlide
            {
                Id = id,
                Name = "Some slide name",
                SlideType = SlideType.Menu
            };
        }

        public DesignSettings GetDesignSettings(string slideId)
        {
            var design = new DesignSettings
            {
                Id = "",
                SlideId = slideId,
                TemplateType = "1",
                SelectedSubTemplate = "1",
                Currency = "EUR"
            };

            design.SubTemplateSettings = this.settingService.GetSubTemplateSettings(design.SubTemplateSettingsId,
                design.SelectedSubTemplate, design.TemplateType);

            var templates = this.GetMenuTemplates();
            design.TemplateTypeOptions = UiHelper.GetTemplateTypeOptions(templates, design.TemplateType);
            design.CurrencyOptions = UiHelper.GetCurrencyOptions(design.Currency);
            design.SubTemplates = this.GetAllSubDesignTemplates(design.SelectedSubTemplate);

            return design;
        }

        public BaseResponse SaveMenu(Menu menu)
        {
            return new BaseResponse {Success = true};
        }

        public BaseResponse DeleteMenu(string id)
        {
            return new BaseResponse { Success = true };
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

        private List<MenuTemplateType> GetMenuTemplates()
        {
            var list = new List<MenuTemplateType>
            {
                new MenuTemplateType {Name = "Single Column", Id = "1"},
                new MenuTemplateType {Name = "Two Columns", Id = "2"},
                new MenuTemplateType {Name = "Three Columns", Id = "3"}
            };

            return list;
        }
        
        private List<SubTemplateSelectionItem> GetAllSubDesignTemplates()
        {
            var all = new List<SubTemplateSelectionItem>();

            all.Add(new SubTemplateSelectionItem { Id = "1", Name = "SingleColumn Basic", ParentTemplateId = "1", HtmlTemplateId = "SingleColumnBasic" });
            all.Add(new SubTemplateSelectionItem { Id = "2", Name = "SingleColumn Bronze", ParentTemplateId = "1", HtmlTemplateId = "SingleColumnBronze" });
            all.Add(new SubTemplateSelectionItem { Id = "3", Name = "SingleColumn Silver", ParentTemplateId = "1", HtmlTemplateId = "SingleColumnSilver" });

            all.Add(new SubTemplateSelectionItem { Id = "4", Name = "TwoColumn 1", ParentTemplateId = "2" });
            all.Add(new SubTemplateSelectionItem { Id = "5", Name = "TwoColumn 2", ParentTemplateId = "2"});
            all.Add(new SubTemplateSelectionItem { Id = "6", Name = "TwoColumn 3", ParentTemplateId = "2" });

            all.Add(new SubTemplateSelectionItem { Id = "7", Name = "ThreeColumn 1", ParentTemplateId = "3" });
            all.Add(new SubTemplateSelectionItem { Id = "8", Name = "ThreeColumn 2", ParentTemplateId = "3"});
            all.Add(new SubTemplateSelectionItem { Id = "9", Name = "ThreeColumn 3", ParentTemplateId = "3" });

            return all;
        }
    }
}