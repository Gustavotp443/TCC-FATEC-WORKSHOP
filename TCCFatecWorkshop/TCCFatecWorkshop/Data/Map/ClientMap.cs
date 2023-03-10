using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("tb_client");

            builder.HasKey(c => c.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(100)
                .IsRequired()
                .HasAnnotation("RegularExpression", @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            builder.Property(c => c.CPFCNPJ)
                .HasMaxLength(18)
                .IsRequired();

            builder.Property(c => c.Document)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Phone)
                .HasMaxLength(20);

            builder.HasMany(x => x.Vehicles)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
