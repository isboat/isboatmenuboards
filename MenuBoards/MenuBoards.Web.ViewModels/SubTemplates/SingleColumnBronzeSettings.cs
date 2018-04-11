namespace MenuBoards.Web.ViewModels.SubTemplates
{
    public class SingleColumnBronzeSettings : ISubTemplateSetting
    {
        public SingleColumnBronzeSettings()
        {
            this.BackgroundColor = "brown";
            this.HeadingBkgdColor = "white";
            this.HeadingColor = "red";
        }

        public string Id { get; set; }
        public string HtmlTemplateId => "SingleColumnBronze";

        public string DesignId { get; set; }

        public string SubTemplateId { get; set; } // RenderingTemplateId id.html as a template

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
    }
}