using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Respponces
{
    public class OrderPartDTO : IMapFrom<OrderPart>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int PartId { get; set; }

        public int Quantity { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderPartDTO, OrderPart>().ReverseMap();
        }



    }
}
