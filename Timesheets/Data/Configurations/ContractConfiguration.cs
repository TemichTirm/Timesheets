using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Models;

namespace Timesheets.Data.Configurations
{
    public class ContractConfiguration: IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("contracts");

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            builder.Property(x => x.Title)
                .ValueGeneratedNever()
                .HasColumnName("Title");

            builder.Property(x => x.DateStart)
                .ValueGeneratedNever()
                .HasColumnName("DateStart");

            builder.Property(x => x.DateEnd)
                .ValueGeneratedNever()
                .HasColumnName("DateEnd");

            builder.Property(x => x.Description)
                .ValueGeneratedNever()
                .HasColumnName("Description");

            builder.Property(x => x.IsDeleted)
                .ValueGeneratedNever()
                .HasColumnName("IsDeleted");
        }
    }
}