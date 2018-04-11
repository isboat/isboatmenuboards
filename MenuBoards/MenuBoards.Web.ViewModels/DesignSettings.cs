using System.Collections.Generic;
using System.Web.Mvc;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.Web.ViewModels
{
    public class DesignSettings
    {
        public string Id { get; set; }

        public string Currency { get; set; }
        
        public string SlideId { get; set; }

        public string TemplateType { get; set; }

        public string SelectedSubTemplate { get; set; }

        public string SubTemplateSettingsId { get; set; }

        public ISubTemplateSetting SubTemplateSettings { get; set; }

        #region Select options

        public IEnumerable<SelectListItem> TemplateTypeOptions { get; set; }

        public IEnumerable<SelectListItem> CurrencyOptions { get; set; }

        public List<SubTemplateSelectionItem> SubTemplates { get; set; }

        #endregion
    }
}