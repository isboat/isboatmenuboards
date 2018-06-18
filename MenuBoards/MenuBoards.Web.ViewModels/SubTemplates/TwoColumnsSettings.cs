using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBoards.Web.ViewModels.SubTemplates
{
    public class TwoColumnsSettings : ITemplateSetting
    {
        public TwoColumnsSettings()
        {
            this.BackgroundColor = "black";
            this.HeadingTextSize = "100px";
        }
        public string Id { get; set; }

        public string DesignId { get; set; }

        public string HtmlTemplateId => "TwoColumn";

        public string SubTemplateId { get; set; }

        public string BackgroundColor { get; set; }

        public string BackgroundImg { get; set; }

        public string HeadingColor { get; set; }

        public string HeadingBkgdColor { get; set; }

        public string HeadingTextSize { get; set; }

        public string MenuItemColor { get; set; }

        public string MenuItemTextSize { get; set; }

        public string MenuItemSubTextSize { get; set; }

        public string MenuItemPriceTextSize { get; set; }

        public string ImageOneUrl { get; set; }

        public string ImageTwoUrl { get; set; }

        public string ImageThreeUrl { get; set; }
    }
}
