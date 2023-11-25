using AutoMapper;
using MongoDB.Bson;
using ProductManagementSystem.Dal.DTOs;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.Dal.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BsonDocument, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["_id"].ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src["Name"]))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src["Description"]))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src["Price"])))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src["CategoryId"].ToString()))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src["Category"]["Name"]))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src["Color"]));
    }
}
