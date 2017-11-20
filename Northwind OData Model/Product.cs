using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.OData.Query;
using GSA.Samples.Northwind.OData.Models;

namespace GSA.Samples.Northwind.OData.Model
{
    [Table("Products")]
    [Page(MaxTop = 1000, PageSize = 50)]
    [Count(Disabled = false)]
    public class Product : BaseEntity, IEntity
    {
        [Key]
        [Column("ProductUniqueID")]
        public override Guid ID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        [Required]
        public Guid ReferenceUniqueID { get; set; }

        public string ProductUri { get; set; }
    }
}