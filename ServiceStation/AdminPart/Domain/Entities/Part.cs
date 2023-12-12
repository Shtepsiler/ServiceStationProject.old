using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Part
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int VendorId { get; set; }
        [NotMapped]
        public Vendor Vendor { get; set; }
        public int StockQty { get; set; }
        [NotMapped]
        public List<PartNeeded> PartNeededs { get; set; }
        [NotMapped]
        public List<OrderPart> OrderedParts { get; set; }
    }
}
