using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFridgeListWebapi.Core.Data.Entities;

namespace MyFridgeListWebapi.Core.Data.Configurations
{
    public sealed class ShoppinglistConfiguration : IEntityTypeConfiguration<Shoppinglist>
    {
        public void Configure(EntityTypeBuilder<Shoppinglist> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder
                .HasMany(x => x.Items)
                .WithOne(x => x.Shoppinglist)
                .HasForeignKey(x => x.ShoppinglistId);
        }
    }
}
