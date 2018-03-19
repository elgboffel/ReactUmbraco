using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace React.Web.Helpers.Extensions
{
    public static class ViewExtensions
    {
        public static RenderModel CreateRenderModel(IPublishedContent content, RouteData routeData)
        {
            var model = new RenderModel(content, CultureInfo.CurrentUICulture);

            //add an umbraco data token so the umbraco view engine executes
            routeData.DataTokens["umbraco"] = model;

            return model;
        }

        // https://stackoverflow.com/a/19208941
        public static string RenderToString(this PartialViewResult partialView)
        {
            var httpContext = HttpContext.Current;

            if (httpContext == null)
                throw new NotSupportedException("An HTTP context is required to render the partial view to a string");

            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

            var stringBuilder = new StringBuilder();

            using (var stringWriter = new StringWriter(stringBuilder))
                using (var htmlStringWriter = new HtmlTextWriter(stringWriter))
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, htmlStringWriter), htmlStringWriter);

            return stringBuilder.ToString();
        }

        public static string RenderToString(this ViewResult partialView)
        {
            var httpContext = HttpContext.Current;

            if (httpContext == null)
                throw new NotSupportedException("An HTTP context is required to render the partial view to a string");

            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

            var stringBuilder = new StringBuilder();

            using (var stringWriter = new StringWriter(stringBuilder))
                using (var htmlTextWriter = new HtmlTextWriter(stringWriter))
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, htmlTextWriter), htmlTextWriter);

            return stringBuilder.ToString();
        }
    }
}
