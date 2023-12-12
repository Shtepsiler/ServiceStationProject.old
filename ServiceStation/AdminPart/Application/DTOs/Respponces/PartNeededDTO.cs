using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Respponces
{
    public class PartNeededDTO : IMapFrom<PartNeeded>
    {
        public int Id { get; set; }
        public int JobId { get; set; }

        public int PartId { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PartNeededDTO, PartNeeded>().ReverseMap();
        }

    }
}
