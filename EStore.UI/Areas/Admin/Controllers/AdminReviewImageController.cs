using EStore.Dto.ReviewImage;
using EStore.Dto.ReviewImage;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminReviewImage")]
    [Authorize(Roles = "Admin,Editor", AuthenticationSchemes = "AdminCookie")]
    public class AdminReviewImageController : BaseController
    {
        private readonly IWriteService<CreateReviewImage, UpdateReviewImage> _writeService;
        private readonly IReadService<ResultReviewImage> _readService;

        public AdminReviewImageController(IWriteService<CreateReviewImage, UpdateReviewImage> writeService, IReadService<ResultReviewImage> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            TempData["ReviewId"] = id;
            var reviewImages = await _readService.GetAll($"ReviewImages/GetReviewImageByReviewId/{id}", "reviewImages");
            return View(reviewImages);
        }

        [HttpGet("[action]")]
        public IActionResult CreateReviewImage()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateReviewImage(CreateReviewImage createReviewImage)
        {
            var result = await HandleServiceAction(async () => await _writeService.UploadImageAsync(createReviewImage.FormFile, createReviewImage.ReviewId, "ReviewImages/CreateReviewImage?ReviewId="), "Review image upload successful.", "Review image upload failed.");
            return RedirectToAction("Index", "AdminReviewImage", new { id = createReviewImage.ReviewId });
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveReviewImage(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "ReviewImages/RemoveReviewImage/"), "Review image removal successful.", "Review image removal failed.");
            return RedirectToAction("Index", "AdminReviewImage", new { id = TempData["ReviewId"].ToString() });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateReviewImage(string id)
        {
            var Review = await _readService.Get("ReviewImages/GetReviewImageById/", id);
            return View(Review);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateReviewImage(UpdateReviewImage updateReviewImage)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateImageAsync(updateReviewImage.FormFile, updateReviewImage.Id, "ReviewImages/UpdateReviewImage?id="), "Review image update successful.", "Review image update failed.");
            return RedirectToAction("Index", "AdminReviewImage", new { id = TempData["ReviewId"].ToString() });
        }
    }
}
