using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Sales.Domain;

namespace Store.Sales.Data.Mapping
{
    public class CouponMapping : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.Code)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // 1 : N => Voucher : Orders
            builder.HasMany(c => c.Orders)
                .WithOne(c => c.Coupon)
                .HasForeignKey(c => c.CouponId);

            builder.ToTable("Coupon");
        }
    }
}
