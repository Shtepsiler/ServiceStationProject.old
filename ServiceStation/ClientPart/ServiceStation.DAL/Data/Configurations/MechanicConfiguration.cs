using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceStation.DAL.Entities;

namespace ServiceStation.DAL.Data.Configurations
{
    public class MechanicConfiguration : IEntityTypeConfiguration<Mechanic>
    {
        public void Configure(EntityTypeBuilder<Mechanic> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.FirstName).HasMaxLength(50);
            builder.Property(p => p.LastName).HasMaxLength(50);
            builder.Property(p => p.Address).HasMaxLength(255);
            builder.Property(p => p.Phone).HasMaxLength(12).IsFixedLength(true);
            builder.Property(p => p.Specialization);
            builder.Property(p=>p.PhotoURL).IsRequired(false);

            builder.HasKey(p => p.Id);
        }
    }
}
