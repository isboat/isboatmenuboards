using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBoards.Web.ViewModels
{
    public class SubTemplateSelectionItem
    {
        public string Id { get; set; }

        public string ParentTemplateId { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }

        public string HtmlTemplateId { get; set; }
    }
}
