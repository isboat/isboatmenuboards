using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Services
{
    public class UiHelper
    {
        public static IEnumerable<SelectListItem> GetCurrencyOptions(string selectedCur)
        {
            var templates = new List<SelectListItem>
            {
                new SelectListItem {Text = "GBP", Value = "GBP"},
                new SelectListItem {Text = "USD", Value = "USD"},
                new SelectListItem {Text = "EUR", Value = "EUR"}
            };

            foreach (var item in templates) item.Selected = item.Value == selectedCur;
            
            return new SelectList(templates, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetTemplateTypeOptions(string selectedItem)
        {
            var list = new List<MenuTemplateType>
            {
                TemplateTypes.GetSingleColumn(),
                TemplateTypes.GetTwoColumn(),
                TemplateTypes.GetThreeColumn()
            };

            var options = list.Select(s => new SelectListItem { Text = s.Name, Value = s.Id, Selected = s.Id == selectedItem }).ToList();

            return options;
        }

        public static IEnumerable<SubTemplateSelectionItem> GetAllSubDesignTemplatesOptions()
        {
            var all = new List<SubTemplateSelectionItem>
            {
                new SubTemplateSelectionItem {Id = "SCBasic", Name = "Single column - Basic", ParentTemplateId = "1"},
                new SubTemplateSelectionItem {Id = "SCBronze", Name = "Single column - Bronze", ParentTemplateId = "1"},
                new SubTemplateSelectionItem {Id = "SCSilver", Name = "Single column - Silver", ParentTemplateId = "1"},
                new SubTemplateSelectionItem {Id = "4", Name = "Two column 1", ParentTemplateId = "2"},
                new SubTemplateSelectionItem {Id = "5", Name = "Two column 2", ParentTemplateId = "2"},
                new SubTemplateSelectionItem {Id = "6", Name = "Two column 3", ParentTemplateId = "2"},
                new SubTemplateSelectionItem {Id = "7", Name = "Three column 1", ParentTemplateId = "3"},
                new SubTemplateSelectionItem {Id = "8", Name = "Three column 2", ParentTemplateId = "3"},
                new SubTemplateSelectionItem {Id = "9", Name = "Three column 3", ParentTemplateId = "3"}
            };

            return all;
        }
    }
}