using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Product
{
    public class ProductFilter
    {
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinPrice { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 9;
    }
}
