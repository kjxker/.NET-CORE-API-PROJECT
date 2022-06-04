using AutoMapper;
using marvel_site_api.AppService;
using marvel_site_api.Model;
using marvel_site_api.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static marvel_site_api.ModelDto.MarvelCharactersDto;

namespace marvel_site_api.Controllers
{
    [ApiController]
    [Route("")]
    public class marvelController : ControllerBase
    {
        marvelAppService appservice = new marvelAppService();
        private readonly IMapper _mapper;
        public marvelController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [Route("characters/{Id}")]
        [HttpGet]
        public async Task<List<MarvelCharacter>> Get(long Id)
        {
            MarvelCharacter response = await appservice.getMarvelCharacterByIdAsync(Id);
            List<MarvelCharacter> ls = new List<MarvelCharacter>();
            ls.Add(response);
            return ls;
        }

        [Route("characters")]
        [HttpGet]
        public async Task<List<MarvelCharacters_Dto>> Get(int limit=100,int offset=0)
        {
            var response = await appservice.getAllMarvelCharacterAsync(limit,offset);
            long totalCount = response.Item1;
            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            Response.Headers.Add("X-Count", response.Item2.Count().ToString());
            return _mapper.Map<List <MarvelCharacters_Dto>>(response.Item2);
        }

    }
}
