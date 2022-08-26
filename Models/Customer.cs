using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Demo.Models
{
    public class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        [NotMapped]
        public string Fullname => Firstname + " " + Lastname;

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
