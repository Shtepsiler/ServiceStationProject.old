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
    public class MechanicDTO : IMapFrom<Mechanic>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Specialization { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MechanicDTO, Mechanic>().ReverseMap();
        }

    }
}
