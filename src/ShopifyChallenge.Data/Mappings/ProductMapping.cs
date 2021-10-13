using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Catalog.Domain;

namespace Store.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            // 1 : N => Products : Images
            builder.HasMany(c => c.ProductImages)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            builder.ToTable("Product");
        }
    }
}
