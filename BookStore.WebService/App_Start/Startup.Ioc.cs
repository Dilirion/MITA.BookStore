using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using BookStore.WebService.App_Start;
using Microsoft.Mef.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;

namespace BookStore.WebService
{
    public partial class Startup
    {
        private void ConfigureIoc()
        {
            var assemblyCatalog = new AssemblyCatalog(GetType().Assembly);
            var directoryCatalog = new DirectoryCatalog("bin");
            var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(assemblyCatalog);
            aggregateCatalog.Catalogs.Add(directoryCatalog);

            var container = new CompositionContainer(aggregateCatalog);
            container.ComposeExportedValue(container);

            var serviceLocator = new MefServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            container.ComposeExportedValue<IServiceLocator>(serviceLocator);
            
            var dependencyResolver = new MefDependencyResolver(serviceLocator);
            DependencyResolver.SetResolver(dependencyResolver);
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
        }
    }
}