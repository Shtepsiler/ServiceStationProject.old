using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServiceStation.DAL.Entities;

namespace ServiceStation.DAL.Data.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {

        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.ManagerId).IsRequired(false);
            builder.Property(p => p.ModelId);
            builder.Property(p => p.Status).HasMaxLength(15).HasDefaultValue("Pending");
            builder.Property(p => p.ClientId);
            builder.Property(p => p.MechanicId).IsRequired(false);
            builder.Property(p => p.IssueDate);
            builder.Property(p => p.FinishDate).IsRequired(false);
            builder.Property(p => p.Description);
            builder.Property(p => p.Price).IsRequired(false);

            builder.HasKey(p => p.Id);

           /* builder.HasCheckConstraint(
                       "constraint_status",
                       "`Status` = 'Pending' or `Status` = 'In Progress'or `Status` = 'Finished'");*/

            builder.HasOne(p => p.Manager).WithMany(p => p.Jobs);
            builder.HasOne(p => p.Model).WithMany(p => p.Jobs);
            builder.HasOne(p => p.Mechanic).WithMany(p => p.Jobs);
            builder.HasOne(p => p.Client).WithMany(p => p.Jobs);


        }
    }
}
