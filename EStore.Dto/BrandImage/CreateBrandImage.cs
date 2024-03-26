using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EStore.Dto.BrandImage
{
    public class CreateBrandImage
    {
        [JsonIgnore]
        public IFormFile FormFile { get; set; }
        public string BrandId { get; set; }
    }
}
