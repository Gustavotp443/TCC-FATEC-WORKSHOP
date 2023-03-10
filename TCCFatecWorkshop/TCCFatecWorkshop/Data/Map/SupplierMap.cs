using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class SupplierMap : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("tb_suppliers");

            builder.HasKey(x=> x.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

            builder
             .Property(s => s.Phone)
             .HasMaxLength(20)
             .IsRequired();

            builder.Property(x=> x.Email)
                .HasMaxLength(150)
                .HasAnnotation("RegularExpression", @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.HasMany(x => x.ProductSuppliers)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
