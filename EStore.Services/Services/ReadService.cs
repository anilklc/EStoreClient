using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EStore.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EStore.Services
{
    public class ReadService<TEntity> : IReadService<TEntity>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ReadService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<TEntity>> GetAll(string apiUrl,string objectName)
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(jsonContent);
            JArray objectArray = (JArray)jsonObject[objectName];
            var result = objectArray.ToObject<List<TEntity>>();
            return result;
        }

        public async Task<TEntity> Get(string apiUrl,string id)
        {
            var response = await _httpClient.GetAsync($"{apiUrl}{id}");
            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TEntity>(jsonContent);

            return result;
        }
    }
}
