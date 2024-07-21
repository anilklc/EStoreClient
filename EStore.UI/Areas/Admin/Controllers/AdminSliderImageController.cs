using EStore.Dto.SliderImage;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminSliderImage")]
    [Authorize(Roles = "Admin,Editor")]
    public class AdminSliderImageController : BaseController
    {
        private readonly IWriteService<CreateSliderImage, UpdateSliderImage> _writeService;
        private readonly IReadService<ResultSliderImage> _readService;

        public AdminSliderImageController(IWriteService<CreateSliderImage, UpdateSliderImage> writeService, IReadService<ResultSliderImage> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            TempData["SliderId"] = id;
            var sliderImages = await _readService.GetAll($"SliderImages/GetSliderImageBySliderId/{id}", "sliderImages");
            return View(sliderImages);
        }

        [HttpGet("[action]")]
        public IActionResult CreateSliderImage()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSliderImage(CreateSliderImage createSliderImage)
        {
            var result = await HandleServiceAction(async () => await _writeService.UploadImageAsync(createSliderImage.FormFile, createSliderImage.SliderId, "SliderImages/CreateSliderImage?SliderId="), "Slider image upload successful.", "Slider image upload failed.");
            return RedirectToAction("Index", "AdminSliderImage", new { id = createSliderImage.SliderId });
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveSliderImage(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "SliderImages/RemoveSliderImage/"), "Slider image removal successful.", "Slider image removal failed.");
            return RedirectToAction("Index", "AdminSliderImage", new { id = TempData["SliderId"].ToString() });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateSliderImage(string id)
        {
            var slider = await _readService.Get("SliderImages/GetSliderImageById/", id);
            return View(slider);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateSliderImage(UpdateSliderImage updateSliderImage)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateImageAsync(updateSliderImage.FormFile, updateSliderImage.Id, "SliderImages/UpdateSliderImage?id="), "Slider image update successful.", "Slider image update failed.");
            return RedirectToAction("Index", "AdminSliderImage", new { id = TempData["SliderId"].ToString() });
        }
    }
}
