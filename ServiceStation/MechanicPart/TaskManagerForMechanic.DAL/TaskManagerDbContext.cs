using Microsoft.EntityFrameworkCore;
using TaskManagerForMechanic.DAL.Config;
using TaskManagerForMechanic.DAL.Entitys;

namespace TaskManagerForMechanic.DAL
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> contextOptions) : base(contextOptions)
        {
        }


        public DbSet<Job> Jobs { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<MechanicsTasks> MechanicsTasks { get; set; }
     


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new MechanicConfiguration());
            modelBuilder.ApplyConfiguration(new MechanicsTasksConfiguration());
           

        }

    }
}
