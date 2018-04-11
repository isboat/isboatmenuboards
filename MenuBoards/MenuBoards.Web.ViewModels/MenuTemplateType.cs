using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MenuBoards.Web.ViewModels
{
    //public enum MenuTemplateType
    //{
    //    [Display( Name = "Single Column")]
    //    SingleColumn = 1,

    //    [Display(Name = "Two Columns")]
    //    TwoColumn = 2,

    //    [Display(Name = "Three Columns")]
    //    ThreeColumn = 3

    //}

    public class MenuTemplateType
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}