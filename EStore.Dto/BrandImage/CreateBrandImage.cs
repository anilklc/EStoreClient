using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.BrandImage
{
    public class CreateBrandImage
    {
        public IFormFile fromFile { get; set; }
        public Guid BrandId { get; set; }
    }
}
