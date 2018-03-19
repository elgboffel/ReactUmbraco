using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace React.Web.Models.React
{
    public class LayoutModel: RenderModel
    {
        public LayoutModel(IPublishedContent content) : base(content)
        {
        }

        public InitialState InitialState { get; set; }
        public string SiteDescription { get; set; }
    }
}
