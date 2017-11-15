using System.Web.Http;
using System.Web.OData.Extensions;

using GSA.Samples.Northwind.OData.Models;

namespace GSA.Samples.Northwind.OData.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: NorthwindModel.GetConventionModel());
        }
    }
}
