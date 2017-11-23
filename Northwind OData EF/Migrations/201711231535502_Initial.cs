namespace GSA.Samples.Northwind.OData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductUniqueID = c.Guid(nullable: false),
                        ProductName = c.String(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReferenceUniqueID = c.Guid(nullable: false, defaultValue:Guid.NewGuid()),
                        ProductUri = c.String(),
                    })
                .PrimaryKey(t => t.ProductUniqueID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
