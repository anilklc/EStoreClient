using EStore.Dto.Product;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EStore.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IReadService<ResultProduct> _readService;


        public ProductsController(IReadService<ResultProduct> readService)
        {
            _readService = readService;
        }

        public async Task<IActionResult> Index(int page = 0)
        {
            var productList = await _readService.GetAllPagination($"Products/GetAllProducts?Page={page}");
            return View(productList);
        }
    }
}
