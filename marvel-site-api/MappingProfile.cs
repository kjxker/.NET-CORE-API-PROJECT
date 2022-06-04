using AutoMapper;
using marvel_site_api.Model;
using static marvel_site_api.ModelDto.MarvelCharactersDto;

namespace marvel_site_api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MarvelCharacters, MarvelCharacters_Dto>().ReverseMap();
        }
    }
}
