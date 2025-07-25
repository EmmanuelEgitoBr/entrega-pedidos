using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Infra.EfMappings;

public class ItemMap : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");
        builder.Property(c => c.ItemId).ValueGeneratedOnAdd();
        builder.HasKey(c => c.ItemId);
    }
}
