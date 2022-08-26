using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Demo.Models
{
    public class OrderInvoice
    {
        public OrderInvoice()
        {
            OrderInventories = new HashSet<OrderInventory>();
        }

        public int Id { get; set; }
        public int Customerid { get; set; }

        [NotMapped]
        public string OrderSummary => Id + " - " + Customer.Fullname;

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderInventory> OrderInventories { get; set; }
    }
}
