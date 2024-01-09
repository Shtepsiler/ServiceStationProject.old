using System.ComponentModel.DataAnnotations.Schema;

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
