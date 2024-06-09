using Microsoft.AspNetCore.Http;

namespace EStore.Dto.ProductImage
{
    public class UpdateProductImage
    {
        public string Id { get; set; }
        public IFormFile FormFile { get; set; }
    }
}