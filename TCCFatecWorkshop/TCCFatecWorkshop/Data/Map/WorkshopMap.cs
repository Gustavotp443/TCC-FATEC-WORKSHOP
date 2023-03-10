using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class WorkshopMap:IEntityTypeConfiguration<Workshop>
    {

        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.ToTable("tb_workshops");

            builder.HasKey(x => x.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(150);
                    

            builder.Property(x => x.Email)
                .HasMaxLength(150)
                .HasAnnotation("RegularExpression", @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


            builder.HasOne(x => x.User)
                .WithMany(x => x.Workshops)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x=> x.Products)
                .WithOne(x=> x.Workshop)
                .HasForeignKey(x=> x.WorkshopId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.Services)
                .WithOne(x=> x.Workshop)
                .HasForeignKey(x=> x.WorkshopId)
                .OnDelete(DeleteBehavior.Cascade);

 

        }
    }
}
