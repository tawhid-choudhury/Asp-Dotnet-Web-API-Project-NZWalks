using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, RegionAddRequestDto>().ReverseMap();
            CreateMap<Region, RegionUpdateRequestDto>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, WalkAddRequestDto>().ReverseMap();
            CreateMap<Walk, WalkUpdateRequestDto>().ReverseMap();

        }
    }
}
