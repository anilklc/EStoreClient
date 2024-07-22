using EStore.Dto.Brand;
using EStore.Dto.Review;
using EStore.Dto.Review;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminReview")]
    [Authorize(Roles = "Admin,Editor")]
    public class AdminReviewController : BaseController
    {
        private readonly IWriteService<CreateReview, UpdateReview> _writeService;
        private readonly IReadService<ResultReview> _readService;

        public AdminReviewController(IWriteService<CreateReview, UpdateReview> writeService, IReadService<ResultReview> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var Reviews = await _readService.GetAll("Reviews/GetAllReviews", "reviews");
            return View(Reviews);
        }

        [HttpGet("[action]")]
        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateReview(CreateReview createReview)
        {

            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createReview, "Reviews/CreateReview"), "Review creation successful.", "Review creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveReview(string id)
        {

            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Reviews/RemoveReview/"), "Review removal successful.", "Review removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateReview(string id)
        {
            var Review = await _readService.Get("Reviews/GetByIdReview/", id);
            return View(Review);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateReview(UpdateReview updateReview)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateReview, "Reviews/UpdateReview/"), "Review update successful.", "Review update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
