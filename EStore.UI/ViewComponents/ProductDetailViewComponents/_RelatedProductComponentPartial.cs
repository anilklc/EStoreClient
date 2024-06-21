using EStore.Dto.Product;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var productList = await _readService.GetAllPagination($"Products/GetAllProductsByFilter");
            return View(productList);
        }
    }
}
