using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.ModelServices
{
    public class UiHelper
    {
        public static IEnumerable<SelectListItem> GetTemplateTypeOptions(List<MenuTemplateType> templateList , string defaultTemplateType)
        {
            var templates = templateList.Select(menuTemplate => new SelectListItem
                {
                    Text = menuTemplate.Name,
                    Value = menuTemplate.Id,
                    Selected = defaultTemplateType == menuTemplate.Id
                })
                .ToList();
            
            return new SelectList(templates, "Value", "Text");
        }
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
    }
}