using System.Collections.Generic;
using System.ComponentModel;

namespace MenuBoards.Web.ViewModels
{
    public class Menu
    {
        public string Id { get; set; }

        public string SlideId { get; set; }

        public int Position { get; set; }

        [DisplayName("Menu Heading")]
        public string MainMenuHeading { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}