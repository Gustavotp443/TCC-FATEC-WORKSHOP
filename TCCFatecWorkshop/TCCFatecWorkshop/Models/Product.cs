namespace TCCFatecWorkshop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public double SalePrice { get; set; }

        public string? Description { get; set; }

        public ICollection<ProductsSupplier> ProductsSuppliers { get; set; } = new List<ProductsSupplier>();

        //ManyToOne
        public Workshop Workshop { get; set; }

        public Product() { }

        public Product(int id, string name, string category, double salePrice, string? description, Workshop workshop)
        {
            Id = id;
            Name = name;
            Category = category;
            SalePrice = salePrice;
            Description = description;
            Workshop = workshop;
        }
    }
}
