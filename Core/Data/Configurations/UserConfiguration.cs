using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFridgeListWebapi.Core.Data.Entities;

namespace MyFridgeListWebapi.Core.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(x => x.Fridges)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder
                .HasMany(x => x.Shoppinglists)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
