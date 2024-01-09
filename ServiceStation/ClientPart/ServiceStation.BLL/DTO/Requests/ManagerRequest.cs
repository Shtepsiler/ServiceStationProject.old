using System.ComponentModel.DataAnnotations;

namespace ServiceStation.BLL.DTO.Requests
{
    public class ManagerRequest
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string FullName { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
