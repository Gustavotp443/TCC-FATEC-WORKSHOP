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
            modelBuilder.ApplyConfiguration(new WorkshopMap());
            modelBuilder.ApplyConfiguration(new  ProductMap());
            modelBuilder.ApplyConfiguration(new ProductsSupplierMap());
            modelBuilder.ApplyConfiguration(new SupplierMap());
            modelBuilder.ApplyConfiguration(new ProductsServiceMap());
            modelBuilder.ApplyConfiguration(new ServiceMap());
            modelBuilder.ApplyConfiguration(new VehicleMap());
            modelBuilder.ApplyConfiguration(new ClientMap());



            base.OnModelCreating(modelBuilder);       
        }
    }
}
