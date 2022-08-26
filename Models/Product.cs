namespace MVC_Demo.Models
{
    public class Product
    {
        public Product()
        {
            InvoiceProducts = new HashSet<InvoiceProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Qoh { get; set; }

        public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; }
    }
}
