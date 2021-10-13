using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Sales.Domain;

namespace Store.Sales.Data.Mapping
{
    public class OrderLineMapping : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProductName)
                .IsRequired()
                .HasColumnType("varchar(250)");

            // 1 : N => Categories : Products
            builder.HasOne(c => c.Order)
                .WithMany(p => p.OrderLines);

            builder.ToTable("OrderLine");
        }
    }
}
