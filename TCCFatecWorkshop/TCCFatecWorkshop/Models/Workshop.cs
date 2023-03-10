using System.ComponentModel.DataAnnotations.Schema;

namespace TCCFatecWorkshop.Models
{
    public class Workshop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }
        public string? Description { get; set; }

        //ManyToOne
        public int UserId { get; set; }
        public User User { get; set; }

        //OneToMany
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public ICollection<Service> Services { get; set; } = new HashSet<Service>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

        public Workshop() 
        {
            CreatedAt = DateTime.UtcNow;
        }

        public Workshop( string name, string? email, string? description, User user) : this()
        {
            Name = name;
            Email = email;
            Description = description;
            User = user;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void RemoveProduct(Product product) 
        {
            var item = Products.FirstOrDefault(x=> x.Id == product.Id);
            if (item != null)
            {
                Products.Remove(item);
            }
        }
    }
}
