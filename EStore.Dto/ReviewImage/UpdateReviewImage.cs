using Microsoft.AspNetCore.Http;

namespace EStore.Dto.ReviewImage
{
    public class UpdateReviewImage
    {
        public string Id { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
