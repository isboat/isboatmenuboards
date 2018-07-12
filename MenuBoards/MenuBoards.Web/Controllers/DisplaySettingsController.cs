using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Core;
using MenuBoards.Interfaces.Web;
using MenuBoards.Services;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.Controllers
{
    public class DisplaySettingsController : Controller
    {
        private readonly IDisplaySettingsService _displaySettingsService = IoC.Container.Resolve<IDisplaySettingsService>();

        // GET: DesignSettings
        public ActionResult Edit(string slideId)
        {
            if (string.IsNullOrEmpty(slideId))
            {
                return RedirectToAction("Index", "Home");
            }

            var settings = this._displaySettingsService.GetDisplaySettings(slideId);

            return View(settings);
        }

        // POST: DesignSettings
        [HttpPost]
        public ActionResult Edit(DisplaySettings settings)
        {
            if (!ValidateRequest)
            {
                return View(settings);
            }

            var response = this._displaySettingsService.SaveDisplaySettings(settings);
            if (response.Success)
            {
                return RedirectToAction("SlideDetails", "Slide", new {id = settings.SlideId});
            }

            return View(settings);
        }
    }
}
