using EStore.Dto.Brand;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminBrand")]
    [Authorize(Roles = "Admin,Editor")]
    public class AdminBrandController : BaseController
    {
        private readonly IWriteService<CreateBrand, UpdateBrand> _writeService;
        private readonly IReadService<ResultBrand> _readService;

        public AdminBrandController(IWriteService<CreateBrand, UpdateBrand> writeService, IReadService<ResultBrand> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var brands = await _readService.GetAll("Brands/GetAllBrands", "brands");
            return View(brands);
        }

        [HttpGet("[action]")]
        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBrand(CreateBrand createBrand)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createBrand, "Brands/CreateBrand"), "Brand creation successful.", "Brand creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveBrand(string id)
        {

            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Brands/RemoveBrand/"), "Brand removal successful.", "Brand removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            var brand = await _readService.Get("Brands/GetByIdBrand/", id);
            return View(brand);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateBrand(UpdateBrand updateBrand)
        {
           var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateBrand, "Brands/UpdateBrand/"), "Brand update successful.", "Brand update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
