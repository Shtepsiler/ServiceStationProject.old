using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Respponces
{
    public class JobDTO : IMapFrom<Job>
    {


        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public int ModelId { get; set; }
        public string? Status { get; set; }
        public int ClientId { get; set; }
        public int? MechanicId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobDTO, Job>().ReverseMap();
        }

    }
}
