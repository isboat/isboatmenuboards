namespace MenuBoards.Web.ViewModels.SubTemplates
{
    public class SingleColumnBasicSettings : ISubTemplateSetting
    {
        public SingleColumnBasicSettings()
        {
            this.BackgroundColor = "white";
            this.HeadingColor = "black";
            this.HeadingBkgdColor = "grey";
        }

        public string Id { get; set; }

        public string DesignId { get; set; }

        public string HtmlTemplateId => "SingleColumnBasic"; // RenderingTemplateId id.html as a template

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

        public string ImageUrl { get; set; }
    }
}