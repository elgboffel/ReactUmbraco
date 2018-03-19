﻿using Autofac;
using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using React.Web.Controllers.React;

namespace React.Web.App_Start
{
    public class UmbracoReactStartup : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // Register custom default controller
            DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(ReactRoutesController));
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // This project uses dependency injection
            // Read more about umbraco and dependency injection here:
            // https://glcheetham.name/2016/05/27/implementing-ioc-in-umbraco-unit-testing-like-a-boss/
            // https://glcheetham.name/2017/01/29/mocking-umbracohelper-using-dependency-injection-the-right-way/


            var umbracoContext = umbracoApplication.Context.GetUmbracoContext();

            var builder = new ContainerBuilder();

            var umbracoHelper = new Umbraco.Web.UmbracoHelper(umbracoContext);

            // Register our controllers from this assembly with Autofac
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // Register controllers from the Umbraco assemblies with Autofac
            builder.RegisterControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);
            // Register the types we need to resolve with Autofac
            builder.RegisterInstance(ExamineManager.Instance).As<ExamineManager>();
            builder.RegisterInstance(umbracoHelper.ContentQuery).As<ITypedPublishedContentQuery>();

            // Set up MVC to use Autofac as a dependency resolver
            var container = builder.Build();
            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // And WebAPI
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
