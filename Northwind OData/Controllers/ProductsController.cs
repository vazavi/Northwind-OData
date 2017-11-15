using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

using Dapper;
using GSA.Samples.Northwind.OData.Models;

namespace GSA.Samples.Northwind.OData.Controllers
{
    public class ProductsController : BaseController<Product>
    {
        [EnableQuery]
        public virtual async Task<IHttpActionResult> Get()
        {
            return Ok(await Db.GetListAsync<Product>());
        }

        [EnableQuery]
        public virtual async Task<IHttpActionResult> Get([FromODataUri] Guid key)
        {
            return Ok((await Db.GetListAsync<Product>(new { ID = key })).FirstOrDefault());
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                await Db.InsertAsync<Guid>(product);
            }
            catch (SqlException e)
            {
                if (e.Message.IndexOf("duplicate", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return Conflict();
                }

                throw;
            }

            return Created(product);
        }

        [EnableQuery]
        public virtual async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Product> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = (await Db.GetListAsync<Product>(new { ID = key })).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            patch.Patch(product);

            await Db.UpdateAsync(product);

            return Updated(product);
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != product.ID)
            {
                return BadRequest();
            }

            await Db.UpdateAsync(product);

            return Updated(product);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            var product = (await Db.GetListAsync<Product>(new { ID = key })).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            await Db.DeleteAsync(product);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}