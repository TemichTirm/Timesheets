using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Models;

namespace Timesheets.Data.Configurations
{
    public class InvoiceConfiguration:IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoices");

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            builder.Property(x => x.DateStart)
                .ValueGeneratedNever()
                .HasColumnName("DateStart");

            builder.Property(x => x.DateEnd)
                .ValueGeneratedNever()
                .HasColumnName("DateEnd");

            builder.Property(x => x.Sum)
                .ValueGeneratedNever()
                .HasColumnName("Sum");

            builder
                .HasOne(invoice => invoice.Contract)
                .WithMany(contract => contract.Invoices)
                .HasForeignKey("ContractId");
        }
    }
}