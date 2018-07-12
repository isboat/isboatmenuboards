using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace MenuBoards.Web.ViewModels
{
    public class SlideDesignSettings
    {
        public string Id { get; set; }

        public string Currency { get; set; }
        
        public string SlideId { get; set; }

        public string TemplateType { get; set; }

        [DisplayName("Template Design")]
        public string SubTemplate { get; set; }

        public List<MenuTemplateType> TemplateTypeOptions { get; set; }

        public IEnumerable<SelectListItem> TemplateOptions { get; set; }
        
        public IEnumerable<SelectListItem> CurrencyOptions { get; set; }

        public List<SubTemplateSelectionItem> SubTemplates { get; set; }
        

        public IEnumerable<SelectListItem> SingleColSubTemplateOptions { get; set; }

        public IEnumerable<SelectListItem> TwoColSubTemplateOptions { get; set; }

        public IEnumerable<SelectListItem> ThreeColSubTemplateOptions { get; set; }


        public string BackgroundColor { get; set; }

        public string BackgroundImg { get; set; }

        public string HeadingColor { get; set; }

        public string HeadingBkgdColor { get; set; }

        public string HeadingTextSize { get; set; }

        public string MenuItemColor { get; set; }

        public string MenuItemTextSize { get; set; }

        public string MenuItemSubTextSize { get; set; }

        public string MenuItemPriceTextSize { get; set; }

        public string ImageUrl { get; set; }



        #region Show flags

        public bool ShowSingleCol => this.SubTemplate == TemplateTypes.GetSingleColumn().Id;

        public bool ShowTwoCol => this.SubTemplate == TemplateTypes.GetTwoColumn().Id;

        public bool ShowThreeCol => this.SubTemplate == TemplateTypes.GetThreeColumn().Id;

        public bool ShowSubTemplate => !string.IsNullOrEmpty(this.SubTemplate);

        #endregion
    }
}