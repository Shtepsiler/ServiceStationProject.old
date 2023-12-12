using ServiceStation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.BLL.DTO.Responses
{
    public class ClientsJobsResponse
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string Status { get; set; }
        public string? MechanicFullName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhone { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

      
    }
}
