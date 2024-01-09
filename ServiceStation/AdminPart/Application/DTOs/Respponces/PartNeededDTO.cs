using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

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
