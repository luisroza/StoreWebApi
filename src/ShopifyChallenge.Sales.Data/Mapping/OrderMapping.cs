using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopifyChallenge.Sales.Domain;

namespace ShopifyChallenge.Sales.Data.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .HasDefaultValueSql("NEXT VALUE FOR OrderSequence");

            // 1 : N => Categories : Products
            builder.HasMany(c => c.OrderLines)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            builder.ToTable("Order");
        }
    }
}
