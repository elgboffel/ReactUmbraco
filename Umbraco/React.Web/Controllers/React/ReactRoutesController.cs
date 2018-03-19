﻿using React.Web.Helpers.Extensions;
using React.Web.Models.React;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace React.Web.Controllers.React
{
    public class ReactRoutesController : ReactRenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            return View("_Layout", CreateLayoutModel());
        }

        /// <summary>
        /// Creates a master model for the current page.
        /// </summary>
        /// <returns>A <see cref="MasterModel"/> object.</returns>
        private LayoutModel CreateLayoutModel()
        {
            var home = CurrentPage.Site();
            var model = new LayoutModel(CurrentPage)
            {
                InitialState = new InitialState
                {
                    content = GetContentPages(),
                    homeUrl = home.Url,
                    location = Request.Url.AbsolutePath,
                    currentContent = new InitialState.CurrentContent
                    {
                        Name = CurrentPage.Name,
                        Url = Request.Url.AbsolutePath,
                        Content = ViewFromContentId(CurrentPage.Id).RenderToString()
                    },
                    currentPageName = CurrentPage.Name
                }
            };

            // Render navigation partials
            //var mainNavigationView = View("Partials/MainNavigation", model);
            //var bottomNavigationView = View("Partials/BottomNavigation", model);
            //model.InitialState.mainNavigation = mainNavigationView.RenderToString();
            //model.InitialState.bottomNavigation = bottomNavigationView.RenderToString();

            return model;
        }

        /// <summary>
        /// Returns a dictionary which maps Umbraco node URLs to basic content objects.
        /// </summary>
        /// <returns>
        /// A <see cref="Dictionary{string, InitialState.Content}"/> object.
        /// </returns>
        private Dictionary<string, InitialState.Content> GetContentPages()
        {
            return Umbraco.TypedContentAtRoot()
                .SelectMany(c =>
                {
                    var list = c.Descendants().ToList();
                    list.Add(c);
                    return list;
                })
                .ToDictionary(p =>
                {
                    var sanitised = p.Url.Replace(@"//", "/");
                    return sanitised;
                }, p => new InitialState.Content { Id = p.Id, Name = p.Name });
        }
    }
}
