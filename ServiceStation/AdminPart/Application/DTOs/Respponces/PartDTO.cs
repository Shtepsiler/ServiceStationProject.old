using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

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
