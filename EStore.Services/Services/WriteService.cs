using EStore.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Services
{
    public class WriteService<TEntity> : IWriteService<TEntity>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WriteService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<TEntity> AddAsync(TEntity entity, string endpoint)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var jsonContent = JsonConvert.SerializeObject(entity);
            var response = await _httpClient.PostAsync(endpoint, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to add entity. Status code: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var addedEntity = JsonConvert.DeserializeObject<TEntity>(jsonResponse);

            return addedEntity;
        }

        public Task DeleteAsync(string id, string endpoint)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(string id, TEntity entity, string endpoint)
        {
            throw new NotImplementedException();
        }
    }
}
