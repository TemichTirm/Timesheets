using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Models;

namespace Timesheets.Data.Configurations
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            builder.Property(x => x.Username)
                .ValueGeneratedNever()
                .HasColumnName("Username");

            builder.Property(x => x.PasswordHash)
                .ValueGeneratedNever()
                .HasColumnName("Password");

            builder.Property(x => x.Role)
                .ValueGeneratedNever()
                .HasColumnName("Role");

        }
    }
}