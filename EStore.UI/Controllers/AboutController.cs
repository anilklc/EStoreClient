using EStore.Dto.About;
using EStore.Dto.Policies;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IReadService<ResultAbout> _readService;

        public AboutController(IReadService<ResultAbout> readService)
        {
            _readService = readService;
        }

        public async Task<IActionResult> Index()
        {
            var abouts = await _readService.GetAll("Abouts/GetAllAbouts", "abouts");
            return View(abouts);
        }
    }
}
