namespace TCCFatecWorkshop.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string? Email { get; set; }
        public string? Description { get; set; }

        public ICollection<ProductsSupplier> ProductSuppliers { get; set; } = new List<ProductsSupplier>();

        public Supplier() { }

        public Supplier(string name, string phone, string? email, string? description)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Description = description;
        }

        public void AddProductSupplier(ProductsSupplier productsSupplier)
        {
            ProductSuppliers.Add(productsSupplier);
        }
    }
}
