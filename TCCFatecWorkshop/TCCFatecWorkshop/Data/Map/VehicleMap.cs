using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("tb_vehicle");

            builder.HasKey(v => v.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(v => v.Model)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.Year)
                .IsRequired();

            builder.Property(v => v.Brand)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.LicencePlate)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(v => v.ChassisNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(v => v.Client)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
