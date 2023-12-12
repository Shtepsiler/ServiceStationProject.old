using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceStation.DAL.Entities;

namespace ServiceStation.DAL.Data.Configurations
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.SerialNumber).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(255);
            builder.Property(p => p.Price);
            builder.Property(p => p.VendorId);
            builder.Property(p => p.StockQty);
            builder.HasKey(p => p.Id);


            builder.HasOne(p => p.Vendor).WithMany(p => p.Parts);

        }
    }
}
