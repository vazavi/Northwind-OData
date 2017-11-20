using System.Web.OData.Builder;
using GSA.Samples.Northwind.OData.Models;
using Microsoft.OData.Edm;

namespace GSA.Samples.Northwind.OData.Model
{
    public class NorthwindModel
    {
        public static IEdmModel GetConventionModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.Namespace = "Demos";

            builder.ContainerName = "DefaultContainer";

            var entitySet = builder.EntitySet<Product>("Products");
            entitySet.EntityType.Select(nameof(Product.ID));
            entitySet.EntityType.Select(nameof(Product.ProductName));
            entitySet.EntityType.Select(nameof(Product.UnitPrice));

            return builder.GetEdmModel();
        }
    }
}