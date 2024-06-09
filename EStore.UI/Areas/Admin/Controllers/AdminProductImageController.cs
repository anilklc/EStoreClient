using EStore.Dto.ProductImage;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminProductImage")]
    public class AdminProductImageController : BaseController
    {
        private readonly IWriteService<CreateProductImage, UpdateProductImage> _writeService;
        private readonly IReadService<ResultProductImage> _readService;

        public AdminProductImageController(IWriteService<CreateProductImage, UpdateProductImage> writeService, IReadService<ResultProductImage> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            TempData["ProductId"] = id;
            var ProductImages = await _readService.GetAll($"ProductImages/GetProductImageByProductId/{id}", "productImages");
            return View(ProductImages);
        }

        [HttpGet("[action]")]
        public IActionResult CreateProductImage()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProductImage(CreateProductImage createProductImage)
        {
            var result = await HandleServiceAction(async () => await _writeService.UploadImageAsync(createProductImage.FormFile, createProductImage.ProductId, "ProductImages/CreateProductImage?ProductId="), "Product image upload successful.", "Product image upload failed.");
            return RedirectToAction("Index", "AdminProductImage", new { id = createProductImage.ProductId });
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveProductImage(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "ProductImages/RemoveProductImage/"), "Product image removal successful.", "Product image removal failed.");
            return RedirectToAction("Index", "AdminProductImage", new { id = TempData["ProductId"].ToString() });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateProductImage(string id)
        {
            var Product = await _readService.Get("ProductImages/GetProductImageById/", id);
            return View(Product);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImage updateProductImage)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateImageAsync(updateProductImage.FormFile, updateProductImage.Id, "ProductImages/UpdateProductImage?id="), "Product image update successful.", "Product image update failed.");
            return RedirectToAction("Index", "AdminProductImage", new { id = TempData["ProductId"].ToString() });
        }
    }
}
