using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class ServiceMap : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("tb_services");

            builder.HasKey(x => x.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);

            builder.Property(x=>x.TotalValue).IsRequired();

            builder.Property(s => s.Situation).IsRequired().HasConversion<string>();

            builder.Property(s => s.InitialDate).IsRequired().HasColumnType("timestamp");

            builder.Property(s => s.FinalDate).IsRequired().HasColumnType("timestamp");

            
            builder.HasOne(s => s.Workshop)
                  .WithMany(w => w.Services)
                  .HasForeignKey(s => s.WorkshopId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Vehicle)
                   .WithMany(v => v.Services)
                   .HasForeignKey(s => s.VehicleId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.ProductsServices)
                   .WithOne(ps => ps.Service)
                   .HasForeignKey(ps => ps.ServiceId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
