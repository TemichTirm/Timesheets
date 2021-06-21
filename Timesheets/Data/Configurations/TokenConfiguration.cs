using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.Models;

namespace Timesheets.Data.Configurations
{
    public class TokenConfiguration:IEntityTypeConfiguration<RefreshToken>
    {
        /// <summary> Конфигурация таблицы в БД для хранения refresh токенов </summary>
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("tokens");

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            builder.Property(x => x.Token)
                .ValueGeneratedNever()
                .HasColumnName("RefreshToken");
        }
    }
}
