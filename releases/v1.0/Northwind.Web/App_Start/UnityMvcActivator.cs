#region

using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Northwind.Web.App_Start;
using WebActivatorEx;

#endregion

[assembly: PreApplicationStartMethod(typeof (UnityWebActivator), "Start")]

namespace Northwind.Web.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            DynamicModuleUtility.RegisterModule(typeof (UnityPerRequestHttpModule));

            // Set default locator
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
        }
    }
}