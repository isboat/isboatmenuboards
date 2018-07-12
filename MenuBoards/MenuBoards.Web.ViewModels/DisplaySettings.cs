using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBoards.Web.ViewModels
{
    public class DisplaySettings
    {
        public string Id { get; set; }

        public string SlideId { get; set; }

        [DisplayName("Go live date and time")]
        public string GoLiveDatetime { get; set; }

        public bool Disable { get; set; }
    }
}
