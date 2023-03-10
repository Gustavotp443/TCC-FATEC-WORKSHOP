using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class ProductsSupplierMap : IEntityTypeConfiguration<ProductsSupplier>
    {
        public void Configure(EntityTypeBuilder<ProductsSupplier> builder)
        {
            builder.ToTable("tb_products_suppliers");

            builder.HasKey(x => new { x.ProductId, x.SupplierId });

            builder.HasOne(p => p.Product)
                .WithMany(pc => pc.ProductsSuppliers)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(x=> x.Supplier)
                .WithMany(x=> x.ProductSuppliers)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder
                .Property(ps => ps.PurchasePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder
                .Property(ps => ps.Quantity)
                .IsRequired();
        }
    }
}
