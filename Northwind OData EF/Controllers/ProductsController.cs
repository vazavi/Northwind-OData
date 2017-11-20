using System;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using GSA.Samples.Northwind.OData.Model;

namespace GSA.Samples.Northwind.OData.Controllers
{
    public class ProductsController : BaseController<Product>
    {
        [EnableQuery]
        public virtual IHttpActionResult Get()
        {
            return Ok(Db.Products);
        }

        [EnableQuery]
        public virtual IHttpActionResult Get([FromODataUri] Guid key)
        {
            return Ok(Db.Products.FirstOrDefault(p => p.ID == key));
        }
    }
}