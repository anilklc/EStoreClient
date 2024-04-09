using EStore.Dto.Footer;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminFooter")]
    public class AdminFooterController : BaseController
    {
        private readonly IWriteService<CreateFooter, UpdateFooter> _writeService;
        private readonly IReadService<ResultFooter> _readService;

        public AdminFooterController(IWriteService<CreateFooter, UpdateFooter> writeService, IReadService<ResultFooter> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var Footers = await _readService.GetAll("Footers/GetAllFooters", "footer");
            return View(Footers);
        }

        [HttpGet("[action]")]
        public IActionResult CreateFooter()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateFooter(CreateFooter createFooter)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createFooter, "Footers/CreateFooter"), "Footer creation successful.", "Footer creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveFooter(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Footers/RemoveFooter/"), "Footer removal successful.", "Footer removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateFooter(string id)
        {
            var Footer = await _readService.Get("Footers/GetByIdFooter/", id);
            return View(Footer);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateFooter(UpdateFooter updateFooter)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateFooter, "Footers/UpdateFooter/"), "Footer update successful.", "Footer update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
