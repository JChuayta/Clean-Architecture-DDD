using AutoMapper;
using CleanArchitecture.Domain;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

namespace CleanArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideosVm>();
        }
    }
}
