using System.Data.Entity;
using GSA.Samples.Northwind.OData.Model;

namespace GSA.Samples.Northwind.OData.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("name=NorthwindContext")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}