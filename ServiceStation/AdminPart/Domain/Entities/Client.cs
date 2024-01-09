using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        [NotMapped]
        public List<Job> Jobs { get; set; }


    }

}
