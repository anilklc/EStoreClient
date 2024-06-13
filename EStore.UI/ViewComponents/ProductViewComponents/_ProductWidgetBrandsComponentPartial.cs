using EStore.Dto.Brand;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.ProductViewComponents
{
    public class _ProductWidgetBrandsComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultBrand> _readService;
        public _ProductWidgetBrandsComponentPartial(IReadService<ResultBrand> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brandImage = await _readService.GetAll("Brands/GetAllBrands", "brands");
            return View(brandImage);
        }
    }
}