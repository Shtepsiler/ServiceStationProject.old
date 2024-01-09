using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Respponces
{
    public class ModelDTO : IMapFrom<Model>
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ModelDTO, Model>().ReverseMap();
        }
    }


}
