using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SneakerShop.Domain.Entities;

namespace SneakerShop.Infrastructure.Data.Config
{
    public class OrderItemConfiguration: IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.ProductVariant)
                .WithMany(pv => pv.OrderItems)
                .HasForeignKey(oi => oi.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}