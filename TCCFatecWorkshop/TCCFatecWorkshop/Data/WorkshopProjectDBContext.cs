using Microsoft.EntityFrameworkCore;
using TCCFatecWorkshop.Data.Map;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data
{
    public class WorkshopProjectDBContext : DbContext
    {
        public WorkshopProjectDBContext(DbContextOptions<WorkshopProjectDBContext> options):base(options){ 
        }
    
        public DbSet<User> Users { get; set; }      //Setting Model as DB entity
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsSupplier> ProductsSuppliers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ProductsService> ProductsServices { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Client> Clients { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MAPS
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("tb_users");
            modelBuilder.Entity<Workshop>().ToTable("tb_workshops");
            modelBuilder.Entity<Product>().ToTable("tb_products");
            modelBuilder.Entity<ProductsSupplier>().ToTable("tb_products_suppliers").HasKey(ps => new { ps.ProductId, ps.SupplierId });
            modelBuilder.Entity<Supplier>().ToTable("tb_suppliers");
            modelBuilder.Entity<Service>().ToTable("tb_services");
            modelBuilder.Entity<ProductsService>().ToTable("tb_products_services").HasKey(ps => new { ps.ProductId, ps.ServiceId }); 
            modelBuilder.Entity<Vehicle>().ToTable("tb_vehicle");
            modelBuilder.Entity<Client>().ToTable("tb_client");

        }
    }
}
