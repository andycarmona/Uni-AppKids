namespace UniAppKids.DNNControllers.Services
{
    using DotNetNuke.Web.Api;

    public class RouterMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(
            "DataExchange",
            "default",
            "{controller}/{action}",
            new string[] { "UniAppKids.DNNControllers.Services" });
        }
    }
}