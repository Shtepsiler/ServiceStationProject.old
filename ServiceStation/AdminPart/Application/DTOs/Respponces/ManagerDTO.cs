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
    public class ManagerDTO : IMapFrom<Manager>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ManagerDTO, Manager>().ReverseMap();
        }

    }
}
