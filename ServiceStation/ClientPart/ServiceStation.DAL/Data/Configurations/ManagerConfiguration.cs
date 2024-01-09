using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceStation.DAL.Entities;

namespace ServiceStation.DAL.Data.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.FullName).HasMaxLength(100);
            builder.Property(p => p.PhoneNumber).HasMaxLength(12);
            builder.Property(p => p.Email).HasMaxLength(50);

            builder.HasKey(p => p.Id);
        }
    }
}
