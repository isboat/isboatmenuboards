using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MenuBoards.Web.ViewModels
{
    public class TemplateTypes
    {
        public static MenuTemplateType GetSingleColumn()
        {
            return new MenuTemplateType("Single Column", "1", "SingleColumn");
        }

        public static MenuTemplateType GetTwoColumn()
        {
            return new MenuTemplateType {Name = "Two Columns", Id = "2", HtmlTemplateId = "TwoColumn"};
        }

        public static MenuTemplateType GetThreeColumn()
        {
            return new MenuTemplateType { Name = "Three Columns", Id = "3", HtmlTemplateId = "ThreeColumn" };
        }

        //[Display(Name = "Two Columns")]
        //TwoColumn = 2,

        //[Display(Name = "Three Columns")]
        //ThreeColumn = 3

    }

    public class MenuTemplateType
    {
        public MenuTemplateType(string name, string id, string htmlTemplateId)
        {
            this.Name = name;
            this.Id = id;
            this.HtmlTemplateId = htmlTemplateId;
        }

        public MenuTemplateType(){}

        public string Id { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }

        public string HtmlTemplateId { get; set; }
    }
}