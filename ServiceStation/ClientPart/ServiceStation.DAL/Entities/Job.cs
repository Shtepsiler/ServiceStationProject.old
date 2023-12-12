using Microsoft.EntityFrameworkCore.Metadata;
using ServiceStation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.DAL.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        [NotMapped]
        public Manager? Manager { get; set; }
        public int ModelId { get; set; }
        [NotMapped]
        public Model Model { get; set; }
        public string? Status { get; set; }
        public int ClientId { get; set; }
        [NotMapped]
        public Client Client { get; set; }
        public int? MechanicId { get; set; }
        [NotMapped]
        public Mechanic? Mechanic { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        [NotMapped]
        public List<Order> Orders { get; set; }
        [NotMapped]
        public List<PartNeeded> PartNeededs { get; set; }
        [NotMapped]
        public List<MechanicsTasks> Tasks { get; set; }
    }
}
