using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Product
{
    public class ResultProduct
    {
        public List<ProductDto> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

    }
}
