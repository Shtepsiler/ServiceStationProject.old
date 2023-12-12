using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Model
    {
        public Model(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<Job> Jobs { get; set; }
    }
}
