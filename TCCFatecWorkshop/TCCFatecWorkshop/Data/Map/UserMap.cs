using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tb_users");

            builder.HasKey(x => x.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).IsRequired().HasMaxLength(30);

            builder.Property(x => x.Email).IsRequired()
                .HasMaxLength(150)
                .HasAnnotation("RegularExpression", @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            builder.HasMany(x=> x.Workshops)
                .WithOne(x=> x.User)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
 