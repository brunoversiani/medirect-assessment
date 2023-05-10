using MeDirectTest.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeDirectTest.Data.Maps
{
    public class TransactionMap : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
            builder.HasKey(x => x.TransactionId);
            builder.Property(x => x.TrClientId).IsRequired().HasMaxLength(300);
            builder.Property(x => x.TrFirstName).HasMaxLength(300);
            builder.Property(x => x.TrLastName).HasMaxLength(300);
            builder.Property(x => x.TrFromCurrency).IsRequired().HasMaxLength(3);
            builder.Property(x => x.TrFromAmount).IsRequired().HasMaxLength(300);
            builder.Property(x => x.TrToCurrency).IsRequired().HasMaxLength(300);
            builder.Property(x => x.TrRate).IsRequired().HasMaxLength(300);
            builder.Property(x => x.TrResult).IsRequired().HasMaxLength(300);
            builder.Property(x => x.TrRateTimestamp).IsRequired().HasMaxLength(300);
            builder.Property(x => x.TransactionTimestamp).IsRequired().HasMaxLength(300);
        }
    }
}





