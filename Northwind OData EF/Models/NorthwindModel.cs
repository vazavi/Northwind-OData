using Microsoft.OData.Edm;
using System.Web.OData.Builder;

namespace GSA.Samples.Northwind.OData.Models
{
    public class NorthwindModel
    {
        public static IEdmModel GetConventionModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.Namespace = "Demos";

            builder.ContainerName = "DefaultContainer";

            var entitySet = builder.EntitySet<Product>("Products");

            return builder.GetEdmModel();
        }
    }
}