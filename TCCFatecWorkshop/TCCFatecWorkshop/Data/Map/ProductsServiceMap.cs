using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class ProductsServiceMap : IEntityTypeConfiguration<ProductsService>
    {
        public void Configure(EntityTypeBuilder<ProductsService> builder)
        {
            builder.ToTable("tb_products_services");

            builder.HasKey(x => new { x.ProductId, x.ServiceId });

            builder.HasOne(x=>x.Product)
                .WithMany(x=> x.ProductsServices)
                .HasForeignKey(x=>x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(x=> x.Service)
                .WithMany(x=>x.ProductsServices)
                .HasForeignKey(x=> x.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
