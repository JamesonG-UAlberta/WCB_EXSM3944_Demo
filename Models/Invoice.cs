using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Demo.Models
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceProducts = new HashSet<InvoiceProduct>();
        }

        public int Id { get; set; }
        public int Customerid { get; set; }

        [NotMapped]
        public string OrderSummary => Id + " - " + Customer.Fullname;

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; }
    }
}
