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
    public class PartDTO : IMapFrom<Part>
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int VendorId { get; set; }

        public int StockQty { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PartDTO, Part>().ReverseMap();
        }


    }
}
