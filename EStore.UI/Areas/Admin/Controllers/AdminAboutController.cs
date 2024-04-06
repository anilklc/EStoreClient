using EStore.Dto.About;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminAbout")]
    public class AdminAboutController : BaseController
    {
        private readonly IWriteService<CreateAbout, UpdateAbout> _writeService;
        private readonly IReadService<ResultAbout> _readService;

        public AdminAboutController(IWriteService<CreateAbout, UpdateAbout> writeService, IReadService<ResultAbout> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var abouts = await _readService.GetAll("Abouts/GetAllAbouts", "abouts");
            return View(abouts);
        }

        [HttpGet("[action]")]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAbout(CreateAbout createAbout)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createAbout, "Abouts/CreateAbout"), "About creation successful.", "About creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveAbout(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Abouts/RemoveAbout/"), "About removal successful.", "About removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            var About = await _readService.Get("Abouts/GetByIdAbout/", id);
            return View(About);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAbout updateAbout)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateAbout, "Abouts/UpdateAbout/"), "About update successful.", "About update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
