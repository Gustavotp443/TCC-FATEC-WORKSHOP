using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("tb_users");
            modelBuilder.Entity<Workshop>().ToTable("tb_workshops");
            modelBuilder.Entity<Product>().ToTable("tb_products");
            modelBuilder.Entity<ProductsSupplier>().ToTable("tb_products_suppliers");
            modelBuilder.Entity<Supplier>().ToTable("tb_suppliers");
        }
    }
}
