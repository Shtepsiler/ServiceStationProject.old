using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using Application.DTOs.Respponces;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Name).HasMaxLength(50);

            builder.HasKey(p => p.Id);

        }
    }
}
