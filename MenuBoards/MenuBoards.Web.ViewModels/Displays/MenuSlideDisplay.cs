namespace MenuBoards.Web.ViewModels.Displays
{
    public class MenuSlideDisplay
    {
        public string DateTimeStamp { get; set; }

        public MenuSlide MenuSlide { get; set; }

        public DesignSettings DesignSettings { get; set; }

        public DisplaySettings DisplaySettings { get; set; }

        public bool IsLive { get; set; }
    }
}