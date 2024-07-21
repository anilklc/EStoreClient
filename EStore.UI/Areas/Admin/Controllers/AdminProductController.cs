using EStore.Dto.BrandImage;
using EStore.Dto.Product;
using EStore.Dto.Product;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminProduct")]
    [Authorize(Roles = "Admin,Editor", AuthenticationSchemes = "AdminCookie")]
    public class AdminProductController : BaseController
    {
        private readonly IWriteService<CreateProduct, object> _writeService;
        private readonly IReadService<ResultProductAdmin> _readService;

        public AdminProductController(IWriteService<CreateProduct, object> writeService, IReadService<ResultProductAdmin> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var products = await _readService.GetAll("Products/GetAllProductsAdmin", "products");
            return View(products);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateProduct()
        {
            var brand = await _readService.GetAll("Brands/GetAllBrands", "brands");
            List<SelectListItem> brandValues = (from x in brand
                                               select new SelectListItem
                                               {
                                                   Text = x.BrandName,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.brandValues = brandValues;
            var category = await _readService.GetAll("Categories/GetAllCategories", "categories");
            List<SelectListItem> categoryValues = (from x in category
                                                   select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.categoryValues = categoryValues;
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct(CreateProduct createProduct)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddWithFileAsync(createProduct, "Products/CreateProduct"), "Product creation successful.", "Product creation failed.");
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
            var brand = await _readService.GetAll("Brands/GetAllBrands", "brands");
            List<SelectListItem> brandValues = (from x in brand
                                                select new SelectListItem
                                                {
                                                    Text = x.BrandName,
                                                    Value = x.Id.ToString()
                                                }).ToList();
            ViewBag.brandValues = brandValues;
            var category = await _readService.GetAll("Categories/GetAllCategories", "categories");
            List<SelectListItem> categoryValues = (from x in category
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
            ViewBag.categoryValues = categoryValues;
            var product = await _readService.Get("Products/GetByIdProduct/", id);
            return View(product);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct updateProduct)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateProduct, "Products/UpdateProduct/"), "Product update successful.", "Product update failed.");
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateProductCoverImage(string id)
        {
            var product = await _readService.Get("Products/GetByIdProduct/", id);
            return View(product);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateProductCoverImage(UpdateProductCoverImage updateProductCoverImage)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateImageAsync(updateProductCoverImage.formFile, updateProductCoverImage.Id,"Products/UpdateCoverProductImage?id="), "Product update successful.", "Product update failed.");
            return RedirectToAction(nameof(Index));
        }

    }
}
