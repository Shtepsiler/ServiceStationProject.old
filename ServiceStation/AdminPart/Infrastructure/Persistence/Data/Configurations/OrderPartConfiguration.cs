using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using Application.DTOs.Respponces;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class OrderPartConfiguration : IEntityTypeConfiguration<OrderPart>
    {
        public void Configure(EntityTypeBuilder<OrderPart> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.OrderId);
            builder.Property(p => p.PartId);
            builder.Property(p => p.Quantity);

            builder.HasKey(p => p.Id);


            builder.HasOne(p => p.Order).WithMany(p => p.OrderParts);
            builder.HasOne(p => p.Part).WithMany(p => p.OrderedParts);

        }
    }
}
