using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Manager : IdentityUser<int>
    {
        public string FullName { get; set; }


        [NotMapped]
        public List<Job> Jobs { get; set; }
    }
}
