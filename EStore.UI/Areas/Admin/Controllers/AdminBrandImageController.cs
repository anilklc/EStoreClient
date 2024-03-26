using EStore.Dto.Brand;
using EStore.Dto.BrandImage;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Reflection;


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
            TempData["BrandId"] = id;
            var brandImages = await _readService.GetAll($"BrandImages/GetBrandImageByBrandId/{id}", "brandImages");
            return View(brandImages);
        }

        [HttpGet("[action]")]
        public IActionResult CreateBrandImage()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBrandImage(CreateBrandImage createBrandImage)
        {      
            var result = await HandleServiceAction(async () => await _writeService.UploadImageAsync(createBrandImage.FormFile,createBrandImage.BrandId, "BrandImages/CreateBrandImage?BrandId="), "Brand image upload successful.", "Brand image upload failed.");
            return RedirectToAction("Index", "AdminBrandImage", new { id = createBrandImage.BrandId });
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveBrandImage(string id)
        { 
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "BrandImages/RemoveBrandImage/"), "Brand image removal successful.", "Brand image removal failed.");
            return RedirectToAction("Index", "AdminBrandImage", new { id = TempData["BrandId"].ToString() });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateBrandImage(string id)
        {
            var brand = await _readService.Get("BrandImages/GetBrandImageById/", id);
            return View(brand);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateBrandImage(UpdateBrandImage updateBrandImage)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateImageAsync(updateBrandImage.formFile,updateBrandImage.Id, "BrandImages/UpdateBrandImage?id="), "Brand image update successful.", "Brand image update failed.");
            return RedirectToAction("Index", "AdminBrandImage", new { id = TempData["BrandId"].ToString() });
        }


    }
}