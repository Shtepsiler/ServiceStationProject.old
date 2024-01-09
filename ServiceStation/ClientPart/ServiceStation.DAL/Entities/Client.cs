
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceStation.DAL.Entities
{
    public class Client : IdentityUser<int>
    {

        // public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string Phone { get; set; }
        //public string Email { get; set; }

        [NotMapped]
        public List<Job> Jobs { get; set; }
        [NotMapped]
        public RefreshToken RefreshToken { get; set; }

    }

}
