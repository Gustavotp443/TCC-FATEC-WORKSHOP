namespace TCCFatecWorkshop.Models
{
    public class ProductsSupplier
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SupplierId { get; set;}
        public Supplier Supplier { get; set; }

        public double PurchasePrice { get; set; }
        public int Quantity { get; set; }
    }
}
