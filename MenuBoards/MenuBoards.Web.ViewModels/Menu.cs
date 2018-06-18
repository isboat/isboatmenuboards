using System.Collections.Generic;

namespace MenuBoards.Web.ViewModels
{
    public class Menu
    {
        public string Id { get; set; }

        public string SlideId { get; set; }

        public string MainMenuHeading { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}