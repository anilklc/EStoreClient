using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace EStore.Dto.SliderImage
{
    public class UpdateSliderImage
    {
        public string Id { get; set; }

        [JsonIgnore]
        public IFormFile FormFile { get; set; }
    }
}
