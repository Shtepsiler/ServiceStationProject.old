using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Manager : IdentityUser<int>
    {
        public string FullName { get; set; }


        [NotMapped]
        public List<Job> Jobs { get; set; }
    }
}
