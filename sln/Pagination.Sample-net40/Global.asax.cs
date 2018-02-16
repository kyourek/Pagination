using System.Web.Mvc;
using System.Web.Routing;

namespace Pagination.Sample {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            var routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}