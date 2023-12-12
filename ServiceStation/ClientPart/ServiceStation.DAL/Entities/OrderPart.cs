using ServiceStation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.DAL.Entities
{
    public class OrderPart
    {
        public int Id { get; set; } 
        public int OrderId { get; set; }
        [NotMapped]
        public Order? Order { get; set; }
        public int PartId { get; set; }
        [NotMapped]
        public Part? Part { get; set; }
        public int Quantity { get; set; }
    }
}
