using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Infra.EfMappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.Property(c => c.ProductId).ValueGeneratedOnAdd();
        builder.HasKey(c => c.ProductId);

        builder.Property(i => i.Price).HasPrecision(18, 2);
        builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
    }
}
