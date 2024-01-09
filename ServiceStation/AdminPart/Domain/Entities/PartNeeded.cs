using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PartNeeded
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        [NotMapped]
        public Job? Job { get; set; }
        public int PartId { get; set; }
        [NotMapped]
        public Part? Part { get; set; }
        public int Quantity { get; set; }
    }
}
