using System.Data.Entity;
using GSA.Samples.Northwind.OData.Model;
using Northwind_OData_Breeze.Data;

namespace GSA.Samples.Northwind.OData.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("name=NorthwindContext")
        {
            Database.SetInitializer(new NorthwindInitialiser());

        }

        public DbSet<Product> Products { get; set; }
    }
}