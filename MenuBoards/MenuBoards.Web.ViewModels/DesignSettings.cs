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

        public string SubTemplate { get; set; }

        public ITemplateSetting TemplateSettings { get; set; }

        #region Select options

        public List<MenuTemplateType> TemplateTypeOptions { get; set; }

        public IEnumerable<SelectListItem> CurrencyOptions { get; set; }

        public List<SubTemplateSelectionItem> SubTemplates { get; set; }

        public List<ITemplateSetting> DefaultTemplateSettings { get; set; }

        #endregion
    }

    public class SaveSingleColDesignSettings: DesignSettings
    {
        public SingleColumnSettings TemplateSettingsValues { get; set; }
    }

    public class SaveTwoColsDesignSettings : DesignSettings
    {
        public TwoColumnsSettings TemplateSettingsValues { get; set; }
    }

    public class SaveThreeColsDesignSettings : DesignSettings
    {
        public ThreeColumnsSettings TemplateSettingsValues { get; set; }
    }
}