using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Web.ViewModels.SubTemplates;

namespace MenuBoards.DataAccess
{
    public class SubTemplateSettingsRepository: ITemplateSettingsRepository
    {
        public SingleColumnSettings GetSingleColumnSettings(string settingsId)
        {
            return new SingleColumnSettings
            {
                Id = "dffdfs",
                BackgroundColor = "black",
                HeadingBkgdColor = "white",
                HeadingColor = "brown",
                MenuItemSubTextSize = "12",
                MenuItemPriceTextSize = "12",
                HeadingTextSize = "14",
                MenuItemTextSize = "12",
                MenuItemColor = "red"
            };
        }
    }
}
