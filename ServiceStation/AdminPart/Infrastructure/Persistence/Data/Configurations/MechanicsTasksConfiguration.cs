﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using Application.DTOs.Respponces;

namespace Infrastructure.Persistence.Data.Configurations
{
    public class MechanicsTasksConfiguration : IEntityTypeConfiguration<MechanicsTasks>
    {
        public void Configure(EntityTypeBuilder<MechanicsTasks> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.MechanicId);
            builder.Property(p => p.JobId).IsRequired(false);
            builder.Property(p => p.Task).HasMaxLength(200);
            builder.Property(p => p.Status).HasMaxLength(20).HasDefaultValue("Pending");

            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Mechanic).WithMany(p => p.Tasks).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Job).WithMany(p => p.Tasks).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
