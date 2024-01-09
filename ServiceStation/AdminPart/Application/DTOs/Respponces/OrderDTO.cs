using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Respponces
{
    public class OrderDTO : IMapFrom<Order>
    {
        public int Id { get; set; }
        public int JobId { get; set; }

        public DateTime IssueDate { get; set; }
        public bool Delivered { get; set; }
        public bool IsOrdered { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderDTO, Order>().ReverseMap();
        }

    }
}
