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

        public Supplier(int id, string name, string phone, string? email, string? description)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Description = description;
        }
    }
}
