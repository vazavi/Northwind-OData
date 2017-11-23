using System.Data.Entity;
using GSA.Samples.Northwind.OData.Model;
using Northwind_OData_Breeze.Data;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GSA.Samples.Northwind.OData.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("NorthwindContext")
        {
            Database.SetInitializer(new NorthwindInitialiser());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }

        public DbSet<Product> Products { get; set; }
    }
}