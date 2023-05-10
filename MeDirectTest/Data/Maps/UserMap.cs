using MeDirectTest.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeDirectTest.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(x => x.ClientId);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(300);
            builder.Property(x => x.LastName).HasMaxLength(300);
        }
    }
}





