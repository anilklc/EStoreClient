using EStore.Dto.Brand;
using EStore.Dto.BrandImage;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminBrandImage")]
    public class AdminBrandImageController : BaseController
    {
        private readonly IWriteService<CreateBrandImage, UpdateBrandImage> _writeService;
        private readonly IReadService<ResultBrandImage> _readService;

        public AdminBrandImageController(IWriteService<CreateBrandImage, UpdateBrandImage> writeService, IReadService<ResultBrandImage> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var brandImages = await _readService.GetAll($"BrandImges/GetBrandImageByBrandId/{id}", "brandImages");
            return View(brandImages);
        }
    
    }
}
