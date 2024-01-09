using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

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
