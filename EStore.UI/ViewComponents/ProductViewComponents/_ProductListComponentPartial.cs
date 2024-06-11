using EStore.Dto.Product;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.ProductViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultProduct> _readService;
       
        public _ProductListComponentPartial(IReadService<ResultProduct> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page)
        {
           
            var productList = await _readService.GetAllPagination($"Products/GetAllProducts?Page={page}");
            return View(productList);
        }
    }

}


