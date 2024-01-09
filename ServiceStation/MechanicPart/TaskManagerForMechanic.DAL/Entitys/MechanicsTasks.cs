using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerForMechanic.DAL.Entitys
{
    public class MechanicsTasks
    {
        public int Id { get; set; }
        public int MechanicId { get; set; }
        [NotMapped]
        public Mechanic? Mechanic { get; set; }
        public int? JobId { get; set; }
        [NotMapped]
        public Job? Job { get; set; }
        public string Task { get; set; }
        public string Status { get; set; }


    }

}
