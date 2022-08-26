namespace MVC_Demo.Models
{
    public class InvoiceProduct
    {
        public int Id { get; set; }
        public int Orderid { get; set; }
        public int Inventoryid { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
