using EStore.Dto.ProductImage;
using EStore.Dto.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Product
{
    public class ResultProductDetail
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public List<ProductImageDto> ProductImages { get; set; }
        public List<StockDto> Stock { get; set; }
    }
}
