using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EStore.Dto.ReviewImage
{
    public class CreateReviewImage
    {
        [JsonIgnore]
        public IFormFile FormFile { get; set; }
        public string ReviewId { get; set; }
    }
}
