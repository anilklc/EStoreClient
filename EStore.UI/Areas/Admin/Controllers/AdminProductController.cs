using EStore.Dto.Product;
using EStore.Dto.Product;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminProduct")]
    public class AdminProductController : BaseController
    {
        private readonly IWriteService<CreateProduct, UpdateProduct> _writeService;
        private readonly IReadService<ResultProductAdmin> _readService;

        public AdminProductController(IWriteService<CreateProduct, UpdateProduct> writeService, IReadService<ResultProductAdmin> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var Products = await _readService.GetAll("Products/GetAllProductsAdmin", "products");
            return View(Products);
        }

        [HttpGet("[action]")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct(CreateProduct createProduct)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createProduct, "Products/CreateProduct"), "Product creation successful.", "Product creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveProduct(string id)
        {

            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Products/RemoveProduct/"), "Product removal successful.", "Product removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var Product = await _readService.Get("Products/GetByIdProduct/", id);
            return View(Product);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct updateProduct)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateProduct, "Products/UpdateProduct/"), "Product update successful.", "Product update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
