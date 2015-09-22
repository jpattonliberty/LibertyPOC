using System.Web.Http;

namespace Liberty.POC.UI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                                   name: "apiDefault",
                                   routeTemplate: "api/{controller}/{action}/{id}",
                                   defaults: new { id = RouteParameter.Optional }
                                );
        }
    }
}
