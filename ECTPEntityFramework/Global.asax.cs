using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ECTPEntityFramework.DataAccess;

namespace ECTPEntityFramework
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var context = new EctpContext();
            context.Database.CreateIfNotExists();
        }
    }
}
