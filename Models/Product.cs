using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Demo.Models
{
    [Table("product")]
    public class Product
    {

        [Key] // PRIMARY KEY
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity is Microsoft's version of AUTO_INCREMENT, EF translates this during migration.
        [Column("id", TypeName = "int(10)")] // Data type for the column.
        public int ProductID { get; set; }

        [Column("category_id", TypeName = "int(10)")]
        [Required]
        public int CategoryID { get; set; }

        [Column("name", TypeName = "varchar(30)")]
        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [Column("qoh", TypeName = "int(10)")]
        [Required]
        public int QuantityOnHand { get; set; }

        [Column("reorderthreshold", TypeName = "int(10)")]
        public int? ReorderTheshold { get; set; }

        [Column("saleprice", TypeName = "decimal(5,2)")]
        [Required]
        public decimal SalePrice { get; set; }

       
        [ForeignKey(nameof(CategoryID))]
        [InverseProperty(nameof(Models.ProductCategory.Products))]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}