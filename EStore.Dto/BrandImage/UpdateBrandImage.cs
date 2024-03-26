using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.BrandImage
{
    public class UpdateBrandImage
    {
        public string Id { get; set; }
        public IFormFile formFile { get; set; }
    }
}
