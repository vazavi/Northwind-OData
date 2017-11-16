using System.Web.OData;
using System.Web.Http;

using GSA.Samples.Northwind.OData.Models;
using GSA.Samples.Northwind.OData.Filters;

namespace GSA.Samples.Northwind.OData.Controllers
{
    [KeyAndSecretBasicAuthentication] // Enable authentication via an ASP.NET Identity user name and password
    [Authorize] // Require some form of authentication
    public class BaseController<E> : ODataController where E : class, IEntity
    {
        private NorthwindContext _db = new NorthwindContext();

        protected virtual NorthwindContext Db
        {
            get
            {
                return _db;
            }
        }

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }
    }
}