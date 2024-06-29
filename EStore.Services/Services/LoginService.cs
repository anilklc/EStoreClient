using EStore.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Services
{
    public class LoginService<TEntity> : ILoginService<TEntity>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public LoginService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<TEntity> Login(string apiUrl,object loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(apiUrl, loginRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }
    }
}
