
using EnterpriseTemplate.Instrumentation;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Dependency), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Dependency), "Shutdown")]

namespace EnterpriseTemplate.Instrumentation
{
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Mvc;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public static class Dependency
    {
        private static readonly Lazy<IUnityContainer> ApplicationContainer = new Lazy<IUnityContainer>(
            () =>
            {
                var container = new UnityContainer();
                UnityConfig.RegisterTypes(container);
                return container;
            });

        public static IUnityContainer Container
        {
            get
            {
                return ApplicationContainer.Value;
            }
        }

        public static void Start()
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(Container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        public static void Shutdown()
        {
            Container.Dispose();
        }
    }
}