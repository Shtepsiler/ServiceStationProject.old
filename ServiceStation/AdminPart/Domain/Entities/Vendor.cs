using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<Part> Parts { get; set; }
    }
}
