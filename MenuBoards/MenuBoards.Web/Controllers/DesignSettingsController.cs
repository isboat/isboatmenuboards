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
    public class DesignSettingsController : Controller
    {
        private readonly IDesignSettingsService _designSettingsService = IoC.Container.Resolve<IDesignSettingsService>();

        // GET: DesignSettings
        public ActionResult Edit(string slideId)
        {
            if (string.IsNullOrEmpty(slideId))
            {
                return RedirectToAction("Index", "Home");
            }

            var settings = this._designSettingsService.GetDesignSettings(slideId);

            PopulateDropdownds(settings);

            return View(settings);
        }

        // POST: DesignSettings
        [HttpPost]
        public ActionResult Edit(SlideDesignSettings settings)
        {
            if (!ValidateRequest)
            {
                PopulateDropdownds(settings);
                return View(settings);
            }

            var response = this._designSettingsService.SaveDesignSettings(settings);
            if (response.Success)
            {
                return RedirectToAction("SlideDetails", "Slide", new {id = settings.SlideId});
            }

            PopulateDropdownds(settings);
            return View(settings);
        }

        private void PopulateDropdownds(SlideDesignSettings settings)
        {
            settings.TemplateOptions = UiHelper.GetTemplateTypeOptions(settings.TemplateType);

            settings.CurrencyOptions = UiHelper.GetCurrencyOptions(settings.Currency);

            var all = UiHelper.GetAllSubDesignTemplatesOptions();

            var singles = new List<SelectListItem>();
            var twos = new List<SelectListItem>();
            var threes = new List<SelectListItem>();

            foreach (var item in all)
            {
                if (item.ParentTemplateId == TemplateTypes.GetSingleColumn().Id)
                {
                    singles.Add(new SelectListItem { Value = item.Id, Text = item.Name, Selected = item.Id == settings.SubTemplate });
                }

                if (item.ParentTemplateId == TemplateTypes.GetTwoColumn().Id)
                {
                    twos.Add(new SelectListItem { Value = item.Id, Text = item.Name, Selected = item.Id == settings.SubTemplate });
                }

                if (item.ParentTemplateId == TemplateTypes.GetThreeColumn().Id)
                {
                    threes.Add(new SelectListItem { Value = item.Id, Text = item.Name, Selected = item.Id == settings.SubTemplate });
                }
            }

            settings.SingleColSubTemplateOptions = singles;
            settings.TwoColSubTemplateOptions = twos;
            settings.ThreeColSubTemplateOptions = threes;
        }
    }
}
