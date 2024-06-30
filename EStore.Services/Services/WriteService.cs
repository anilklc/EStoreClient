using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Services
{
    public class WriteService<TCreateEntity, TUpdateEntity> : IWriteService<TCreateEntity, TUpdateEntity>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WriteService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpContextAccessor = contextAccessor;
            var accessToken = _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<TCreateEntity> AddWithFileAsync(TCreateEntity entity, string endpoint)
        {
            using var formData = new MultipartFormDataContent();
            foreach (var prop in typeof(TCreateEntity).GetProperties())
            {
                var value = prop.GetValue(entity);
                if (value != null)
                {
                    if (value is IFormFile file)
                    {
                        formData.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                    }
                    else
                    {
                        formData.Add(new StringContent(value.ToString()), prop.Name);
                    }
                }
            }
            var response = await _httpClient.PostAsync(endpoint, formData);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to add entity. Status code: {response.StatusCode}");
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var addedEntity = JsonConvert.DeserializeObject<TCreateEntity>(jsonResponse);
            return addedEntity;
        }
        public async Task<TCreateEntity> AddAsync(TCreateEntity entity, string endpoint)
        {
            var jsonContent = JsonConvert.SerializeObject(entity);
            var response = await _httpClient.PostAsync(endpoint, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to add entity. Status code: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var addedEntity = JsonConvert.DeserializeObject<TCreateEntity>(jsonResponse);

            return addedEntity;
        }
        public async Task DeleteAsync(string id, string endpoint)
        {

            var response = await _httpClient.DeleteAsync($"{endpoint}{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to deleted entity. Status code: {response.StatusCode}");

            }
        }

        public async Task<TUpdateEntity> UpdateAsync(TUpdateEntity entity, string endpoint)
        {

            var jsonData = JsonConvert.SerializeObject(entity);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{endpoint}", stringContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to update entity. Status code: {response.StatusCode}");
            }

            var updatedEntityJson = await response.Content.ReadAsStringAsync();
            var updatedEntity = JsonConvert.DeserializeObject<TUpdateEntity>(updatedEntityJson);

            return updatedEntity;
        }
        public async Task<TCreateEntity> UploadImageAsync(IFormFile formFile, string id, string endpoint)
        {
            using var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(formFile.OpenReadStream()), "FormFile", formFile.FileName);

            var response = await _httpClient.PostAsync($"{endpoint}{id}", formData);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var addedEntity = JsonConvert.DeserializeObject<TCreateEntity>(jsonResponse);
                return addedEntity;
            }
            else
            {
                throw new HttpRequestException($"Resim yükleme başarısız oldu. Status code: {response.StatusCode}");
            }
        }

        public async Task<TUpdateEntity> UpdateImageAsync(IFormFile formFile, string id, string endpoint)
        {
            using var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(formFile.OpenReadStream()), "FormFile", formFile.FileName);

            var response = await _httpClient.PutAsync($"{endpoint}{id}", formData);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var addedEntity = JsonConvert.DeserializeObject<TUpdateEntity>(jsonResponse);
                return addedEntity;
            }
            else
            {
                throw new HttpRequestException($"Resim yükleme başarısız oldu. Status code: {response.StatusCode}");
            }
        }
    }
}
