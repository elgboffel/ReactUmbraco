using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace React.Web.Helpers
{
    /// <summary>
    /// Handles server side routing for React Router
    /// </summary>
    public class ContentFinder : IContentFinder
    {
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            var umbracoHelper = new UmbracoHelper(contentRequest.RoutingContext.UmbracoContext);

            var home = umbracoHelper.ContentAtRoot().FirstOrDefault();

            // Catch all requests and leave to React Router to handle
            contentRequest.PublishedContent = umbracoHelper.TypedContent(home.Id);
            return true;
        }
    }
}
