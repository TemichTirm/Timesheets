using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Models;

namespace Timesheets.Data.Configurations
{
    public class EmployeeConfiguration:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees");

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            builder.Property(x => x.Name)
                .ValueGeneratedNever()
                .HasColumnName("Name");

            builder.Property(x => x.IsDeleted)
                .ValueGeneratedNever()
                .HasColumnName("IsDeleted");

            builder
                 .HasOne(x => x.User)
                 .WithOne()
                 .HasForeignKey<Employee>("UserId");
        }
    }
}