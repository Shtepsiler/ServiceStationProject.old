using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections;
using TaskManagerForMechanic.DAL;
using TaskManagerForMechanic.DAL.Entitys;

using TaskManagerForMechanic.WEB.Extensions;

namespace TaskManagerForMechanic.WEB.GraphQl
{
    public class Query
    {

        [UseApplicationDbContext]
        public List<Job> GetJobs([ScopedService] TaskManagerDbContext context) =>
            context.Jobs.ToList();
        [UseApplicationDbContext]

        public List<Mechanic> GetMechanics([ScopedService] TaskManagerDbContext context) =>
            context.Mechanics.ToList();
        [UseApplicationDbContext]

        public List<MechanicsTasks> GetMechanicsTasks([ScopedService] TaskManagerDbContext context) =>
             context.MechanicsTasks.ToList();



        [UseApplicationDbContext]

        public Mechanic GetMechanic([ScopedService] TaskManagerDbContext context, int id)
        {

            var mechanic = context.Mechanics.Where(p => p.Id == id).First();
            return mechanic;
        }


        [UseApplicationDbContext]

        public Job GetJob([ScopedService] TaskManagerDbContext context, int id)
        {

            var mechanic = context.Jobs.Where(p => p.Id == id).First();
            return mechanic;
        }



        [UseApplicationDbContext]

        public MechanicsTasks GetMechanicsTaskById([ScopedService] TaskManagerDbContext context, int id)
        {

            var Task = context.MechanicsTasks.Where(p => p.Id == id).First();
            return Task;
        }

        [UseApplicationDbContext]

        public List<MechanicsTasks> GetMechanicsTaskByMechanicId([ScopedService] TaskManagerDbContext context, int MechanicId)
        {

            var Task = context.MechanicsTasks.Where(p => p.MechanicId == MechanicId).ToList();
            return Task;
        }

        [UseApplicationDbContext]

        public List<MechanicsTasks> GetMechanicsTaskByTask([ScopedService] TaskManagerDbContext context, string task)
        {

            var Task = context.MechanicsTasks.Where(p => p.Task.Contains(task)).ToList();
            return Task;
        }





        [UseApplicationDbContext]
        public async Task<Mechanic>  Login([ScopedService] TaskManagerDbContext context, string phone, string password)
        {

            var mechanic = context.Mechanics.Where(p => p.Phone == phone).First();
            if(mechanic.Password == password)
            {
                return mechanic;
            }
            return null; 


        }







    }
}
