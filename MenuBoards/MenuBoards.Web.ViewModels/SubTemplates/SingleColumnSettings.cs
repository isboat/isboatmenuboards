namespace MenuBoards.Web.ViewModels.SubTemplates
{
    public class SingleColumnSettings: ITemplateSetting
    {
        public SingleColumnSettings()
        {
            this.BackgroundColor = "white";

            // Heading
            this.HeadingBkgdColor = "saddlebrown";
            this.HeadingColor = "white";
            this.HeadingTextSize = "20";

            // Menu item
            this.MenuItemColor = "black";
            this.MenuItemTextSize = "15";
            this.MenuItemPriceTextSize= "15";
            this.MenuItemSubTextSize = "13";
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

        public string HtmlTemplateId => "SingleColumn";
    }
}