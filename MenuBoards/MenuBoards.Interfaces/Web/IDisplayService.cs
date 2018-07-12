using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuBoards.Web.ViewModels;
using MenuBoards.Web.ViewModels.Displays;

namespace MenuBoards.Interfaces.Web
{
    public interface IDisplayService
    {
        MenuSlideDisplay GetMenuSlide(string slideId, bool previewMode);

        DisplayCodeResponse VerifyDisplayCode(DisplayCode code);

        string GetTimeStamp(string slideId);
        IEnumerable<Slide> LoadVisibleSlides(string account);
    }
}
