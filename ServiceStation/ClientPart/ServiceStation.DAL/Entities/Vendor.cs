using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.DAL.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<Part> Parts { get; set; }
    }
}
