using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using marvel_site_api.Model;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace marvel_site_api.AppService
{
    public class marvelAppService
    {
        private readonly Uri url = new Uri("https://gateway.marvel.com:443/");
        private readonly IConfiguration _config;


        public async Task<MarvelCharacter> getMarvelCharacterByIdAsync(long Id)
        {
            MarvelCharacter character = new MarvelCharacter();
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage responses = await client.GetAsync($"v1/public/characters/{Id}?apikey=ec3461b5a1e95faa06a9fd456069477b&hash=e9d5162ec8a513ac5e1b339828121f62&ts=1");
                    if (responses.IsSuccessStatusCode)
                    {
                        var charObj = await responses.Content.ReadAsStringAsync();
                        JObject jObject  = JsonConvert.DeserializeObject<JObject>(charObj);
                        character.id = (string)jObject["data"]["results"][0]["id"];
                        character.name = (string)jObject["data"]["results"][0]["name"];
                        character.desc = (string)jObject["data"]["results"][0]["description"];
                        return character;
                    }
                    else
                    {
                        //ErrorMessageDto deserializeError = JsonConvert.DeserializeObject<ErrorMessageDto>(response.Content.ReadAsStringAsync().Result);
                        throw new Exception("Failed to retrived character");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        public async Task<Tuple<long,List<MarvelCharacters>>> getAllMarvelCharacterAsync(int limit,int offset)
        {
            if (limit > 100)
            {
                throw new Exception("Limit should not more than 100");
            }
            List<MarvelCharacters> lists = new List<MarvelCharacters>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    
                    HttpResponseMessage responses = await client.GetAsync($"v1/public/characters?apikey=ec3461b5a1e95faa06a9fd456069477b&hash=e9d5162ec8a513ac5e1b339828121f62&ts=1&limit={limit}&offset={offset}");
                    if (responses.IsSuccessStatusCode)
                    {
                        var charObj = await responses.Content.ReadAsStringAsync();
                        JObject jObject = JsonConvert.DeserializeObject<JObject>(charObj);
                        long totalCount = (long)jObject["data"]["total"];
                        JArray characters = (JArray)jObject["data"]["results"];
                        lists = characters.ToObject<List<MarvelCharacters>>();

                        return new Tuple<long, List<MarvelCharacters>>(totalCount, lists);
                    }
                    else
                    {
                        //ErrorMessageDto deserializeError = JsonConvert.DeserializeObject<ErrorMessageDto>(response.Content.ReadAsStringAsync().Result);
                        throw new Exception("Failed to retrived character");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}
