using System.ComponentModel.DataAnnotations;

namespace ServiceStation.BLL.DTO.Requests
{
    public class NewJobRequest
    {
        [Required]
        public string ModelName { get; set; }
        [Required]

        public int ClientId { get; set; }
        [Required]

        public DateTime IssueDate { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
