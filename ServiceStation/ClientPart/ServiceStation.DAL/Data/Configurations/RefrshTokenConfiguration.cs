using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceStation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ServiceStation.DAL.Data.Configurations
{
    public class RefrshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            //builder.Property<int>(p => p.).UseIdentityColumn();
            builder.Property(p => p.ClientSecret);
            builder.Property(p => p.ClientName).HasMaxLength(256);
            builder.Property(p => p.ExpirationDate);

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Client).WithOne(p => p.RefreshToken).HasForeignKey<RefreshToken>(p=>p.ClientName).HasPrincipalKey<Client>(p=>p.UserName).HasConstraintName("FK_Client_Token");
        }
    }
}
