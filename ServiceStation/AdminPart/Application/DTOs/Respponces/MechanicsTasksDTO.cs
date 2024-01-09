using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Respponces
{
    public class MechanicsTasksDTO : IMapFrom<MechanicsTasks>
    {
        public int Id { get; set; }
        public int MechanicId { get; set; }

        public int? JobId { get; set; }

        public string Task { get; set; }
        public string? Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MechanicsTasksDTO, MechanicsTasks>().ReverseMap();
        }

    }

}
