using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Infra.EfMappings;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.Property(c => c.CustomerId).ValueGeneratedOnAdd();
        builder.HasKey(c => c.CustomerId);
    }
}
