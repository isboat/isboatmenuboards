namespace MenuBoards.Web.ViewModels.SubTemplates
{
    public class SingleColumnSettings: ITemplateSetting
    {
        public SingleColumnSettings()
        {
            this.BackgroundColor = "blue";
            this.HeadingBkgdColor = "white";
            this.HeadingColor = "red";
        }

        public string Id { get; set; }

        public string DesignId { get; set; }

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