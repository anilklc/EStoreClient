using EStore.Dto.Login;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;

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
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }
    }
}
