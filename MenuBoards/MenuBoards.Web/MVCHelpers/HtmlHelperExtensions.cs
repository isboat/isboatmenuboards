using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Web.ViewModels;

namespace MenuBoards.Web.MVCHelpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DisplayNone(this HtmlHelper htmlHelper, bool condition)
        {
            return Attribute(condition, "style", "display: none;");
        }

        public static MvcHtmlString DropDownListForSubTemplate(this HtmlHelper htmlHelper, IEnumerable<SubTemplateSelectionItem> options)
        {
            var html = $"< select class=\"form - control\" id=\"SubTemplate\" name=\"SubTemplate\">";

            foreach (var option in options)
            {
                var opt =
                    $"<option value=\"{option.Id}\" {(option.Selected ? "selected=\"selected\"" : "")} data-pid=\"{option.ParentTemplateId}\">{option.Name}</option>";

                html += opt;
            }

            html += "</select>";
            return  new MvcHtmlString(html);
        }

        private static MvcHtmlString Attribute(bool condition, string name, string value)
        {
            name = HttpContext.Current.Server.HtmlEncode(name);
            value = HttpContext.Current.Server.HtmlEncode(value);

            return MvcHtmlString.Create(condition ? name + "=\"" + value + "\"" : string.Empty);
        }
    }
}