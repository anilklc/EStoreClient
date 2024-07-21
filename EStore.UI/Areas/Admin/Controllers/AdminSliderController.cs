using EStore.Dto.BrandImage;
using EStore.Dto.Slider;
using EStore.Dto.Slider;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("Admin/AdminSlider")]
    [Authorize(Roles = "Admin,Editor", AuthenticationSchemes = "AdminCookie")]
    public class AdminSliderController : BaseController
    {
        private readonly IWriteService<CreateSlider, UpdateSlider> _writeService;
        private readonly IReadService<ResultSlider> _readService;

        public AdminSliderController(IWriteService<CreateSlider, UpdateSlider> writeService, IReadService<ResultSlider> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var Sliders = await _readService.GetAll("Sliders/GetAllSliders", "sliders");
            return View(Sliders);
        }

        [HttpGet("[action]")]
        public IActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSlider(CreateSlider createSlider)
        {
           
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createSlider, "Sliders/CreateSlider"), "Slider creation successful.", "Slider creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveSlider(string id)
        {

            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Sliders/RemoveSlider/"), "Slider removal successful.", "Slider removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateSlider(string id)
        {
            var Slider = await _readService.Get("Sliders/GetByIdSlider/", id);
            return View(Slider);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateSlider(UpdateSlider updateSlider)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateSlider, "Sliders/UpdateSlider/"), "Slider update successful.", "Slider update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
