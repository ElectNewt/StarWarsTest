using Shared.Utils.Serialization;
using StarWars.Data.External.Model;
using StarWars.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarWars.Data.External
{
    /// <summary>
    /// Generic controller which will query swapi
    /// </summary>
    /// <typeparam name="T">Type of object to get</typeparam>
    public class SwApi<T> 
    {
        private readonly string _intialUrl;
        private readonly ISerializer _serializer;
        private static readonly IReadOnlyDictionary<Type, string> endpoints = new Dictionary<Type, string>
                {
                    { typeof(Starship), "https://swapi.dev/api/starships/" },

                };


        public SwApi(ISerializer serializer)
        {
            endpoints.TryGetValue(typeof(T), out _intialUrl);
            _serializer = serializer;
        }

        public async Task<ReadOnlyCollection<T>> GetAllItems()
        {
            List<T> results = new List<T>();
            string next = _intialUrl;
            do
            {
                var getResult = await DoGetQuery(next);
                ApiResultDto<T> apiResult = _serializer.DeserializeObject<ApiResultDto<T>>(getResult);
                next = apiResult.next;
                results.AddRange(apiResult.results);
            } while (!string.IsNullOrWhiteSpace(next));

            return results.AsReadOnly();
        }


        private async Task<string> DoGetQuery(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }

        }
    }
}
