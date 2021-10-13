using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Catalog.Domain;

namespace Store.Catalog.Data.Mappings
{
    public class ProductImageMapping : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {

            builder.HasKey(c => c.Id);

            builder.ToTable("ProductImage");
        }
    }
}
