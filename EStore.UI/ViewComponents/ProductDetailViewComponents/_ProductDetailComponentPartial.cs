using EStore.Dto.Brand;
using EStore.Dto.Product;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultProductDetail> _readService;
        public _ProductDetailComponentPartial(IReadService<ResultProductDetail> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var productDetail = await _readService.GetAll($"Products/GetProductDetail?Id={id}", "productDetail");
            return View(productDetail);
            
        }
    }
}
