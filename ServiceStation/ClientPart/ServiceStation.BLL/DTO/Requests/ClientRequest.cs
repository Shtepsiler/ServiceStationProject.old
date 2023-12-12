using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.BLL.DTO.Requests
{
    public class ClientRequest
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? ClientName { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
