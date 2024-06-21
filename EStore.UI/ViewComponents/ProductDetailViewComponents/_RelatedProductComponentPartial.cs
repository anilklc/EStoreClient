using EStore.Dto.Product;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace EStore.UI.ViewComponents.ProductDetailViewComponents
{
    public class _RelatedProductComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultProduct> _readService;

        public _RelatedProductComponentPartial(IReadService<ResultProduct> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryId)
        {
            
            var productList = await _readService.GetAll($"Products/GetProductByCategoryId/{categoryId}", "getProductByCategories");
            return View(productList);
        }
    }
}
