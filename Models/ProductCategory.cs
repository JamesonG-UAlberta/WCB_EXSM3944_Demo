using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Demo.Models
{
    [Table("product_category")]
    public class ProductCategory
    {
        [Key] // PRIMARY KEY
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity is Microsoft's version of AUTO_INCREMENT, EF translates this during migration.
        [Column("id", TypeName = "int(10)")] // Data type for the column.
        public int ProductCategoryID { get; set; }

        [Column("name", TypeName = "varchar(30)")]
        [StringLength(30)]
        [Required]
        public string? Name { get; set; }

        [Column("description", TypeName = "varchar(50)")]
        [StringLength(50)]
        public string? Description { get; set; }
        [InverseProperty(nameof(Models.Product.ProductCategory))]
        public virtual ICollection<Product> Products { get; set; }
    }
}