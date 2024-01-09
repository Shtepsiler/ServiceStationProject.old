using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceStation.DAL.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


        [NotMapped]
        public List<Job> Jobs { get; set; }
    }
}
