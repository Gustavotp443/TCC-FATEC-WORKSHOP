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
        public ICollection<ProductsService> ProductsServices { get; set; } = new List<ProductsService>();

        //ManyToOne
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }

        public Product() { }

        public Product(string name, string category, double salePrice, string? description, Workshop workshop)
        {
            Name = name;
            Category = category;
            SalePrice = salePrice;
            Description = description;
            Workshop = workshop;
        }

        public void AddProductSupplier(ProductsSupplier productsSupplier)
        {
            ProductsSuppliers.Add(productsSupplier);
        }

        public void AddProductService(ProductsService productsService)
        {
            ProductsServices.Add(productsService);
        }
    }
}
