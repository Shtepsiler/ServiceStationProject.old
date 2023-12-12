using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Data.Configurations;
using Microsoft.AspNetCore.Identity;

namespace ServiceStation.DAL.Data
{
    public class ServiceStationDContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {    
        public ServiceStationDContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<MechanicsTasks> MechanicsTasks { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<OrderPart> OrderParts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartNeeded> PartsNeeded { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new ManagerConfiguration());
            modelBuilder.ApplyConfiguration(new MechanicConfiguration());
            modelBuilder.ApplyConfiguration(new MechanicsTasksConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderPartConfiguration());
            modelBuilder.ApplyConfiguration(new PartConfiguration());
            modelBuilder.ApplyConfiguration(new PartNeededConfiguration());
            modelBuilder.ApplyConfiguration(new VendorConfiguration());
            modelBuilder.ApplyConfiguration(new RefrshTokenConfiguration());

        }

    }
}
