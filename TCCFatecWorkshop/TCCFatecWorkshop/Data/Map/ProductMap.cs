using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("tb_products");

            builder.HasKey(p => p.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.SalePrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.HasOne(p => p.Workshop)
                .WithMany(w => w.Products)
                .HasForeignKey(p => p.WorkshopId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProductsSuppliers)
                .WithOne(ps => ps.Product)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProductsServices)
                .WithOne(ps => ps.Product)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
