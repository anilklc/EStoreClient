using EStore.Dto.BrandImage;
using EStore.Dto.Footer;
using EStore.Dto.Stock;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminProductStock")]
    [Authorize(Roles = "Admin,Editor")]
    public class AdminProductStockController : BaseController
    {
        private readonly IWriteService<CreateStock, UpdateStock> _writeService;
        private readonly IReadService<ResultStock> _readService;
        public AdminProductStockController(IWriteService<CreateStock, UpdateStock> writeService, IReadService<ResultStock> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            TempData["ProductId"] = id;
            var stocks = await _readService.GetAll($"Stocks/GetAllStockByProductId/{id}", "stocks");
            return View(stocks);
        }

        [HttpGet("[action]")]
        public IActionResult CreateProductStock()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProductStock(CreateStock createStock)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createStock, "Stocks/CreateStock"), "Stock creation successful.", "Stock creation failed.");
            return RedirectToAction("Index", "AdminProductStock", new { id = createStock.ProductId });
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveProductStock(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Stocks/RemoveStock/"), "Stock removal successful.", "Stock removal failed.");
            return RedirectToAction("Index", "AdminProductStock", new { id = TempData["ProductId"].ToString() });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateProductStock(string id)
        {
            var stock = await _readService.Get("Stocks/GetStockById/", id);
            return View(stock);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateProductStock(UpdateStock updateStock)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateStock, "Stocks/UpdateStock/"), "Stock update successful.", "Stock update failed.");
            return RedirectToAction("Index", "AdminProductStock", new { id = TempData["ProductId"] });
        }

    }
}
