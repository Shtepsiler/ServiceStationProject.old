using ServiceStation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.BLL.DTO.Responses
{
    public class JobResponse
    {
            public int Id { get; set; }
            public int ManagerId { get; set; }         
            public int ModelId { get; set; }          
            public string Status { get; set; }
            public int ClientId { get; set; }          
            public int? MechanicId { get; set; }           
            public DateTime IssueDate { get; set; }
            public DateTime? FinishDate { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
          
    }
    
}
