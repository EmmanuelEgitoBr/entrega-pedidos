using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Delivery.App.Infra.EfMappings;

public class OrderMap : IEntityTypeConfiguration<Order.Delivery.App.Domain.Aggregates.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Order> builder)
    {
        builder.ToTable("Orders");
        builder.Property(c => c.OrderId).ValueGeneratedOnAdd();
        builder.HasKey(c => c.OrderId);

        builder.Property(i => i.TotalPrice).HasPrecision(18,2);
        builder.Property(i => i.TotalPrice).HasColumnType("decimal(18,2)");
    }
}
