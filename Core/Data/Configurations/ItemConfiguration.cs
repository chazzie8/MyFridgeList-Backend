using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFridgeListWebapi.Core.Data.Entities;

namespace MyFridgeListWebapi.Core.Data.Configurations
{
    public sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(entity => entity.Id);
        }
    }
}
