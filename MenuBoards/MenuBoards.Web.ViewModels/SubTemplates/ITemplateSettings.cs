using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBoards.Web.ViewModels.SubTemplates
{
    public interface ITemplateSetting
    {
        string HtmlTemplateId { get; }

        string Id { get; set; }

        string DesignId { get; set; }
    }
}
