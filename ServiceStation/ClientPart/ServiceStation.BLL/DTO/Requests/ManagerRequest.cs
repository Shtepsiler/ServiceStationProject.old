using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
