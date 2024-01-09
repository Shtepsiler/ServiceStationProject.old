using System.ComponentModel.DataAnnotations;

namespace ServiceStation.BLL.DTO.Requests
{
    public class ModelRequest
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
