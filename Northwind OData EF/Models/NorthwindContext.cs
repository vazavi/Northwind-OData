using System.Data.Entity;

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