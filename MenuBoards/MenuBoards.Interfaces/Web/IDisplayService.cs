using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Interfaces.Web
{
    public interface IDisplayService
    {
        MenuSlideDisplay GetMenuSlide(string slideId);

        string GetDateTimeStamp(string slideId);
    }
}
