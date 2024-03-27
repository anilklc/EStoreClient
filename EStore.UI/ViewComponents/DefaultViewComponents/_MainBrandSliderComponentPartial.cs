using EStore.Dto.Brand;
using EStore.Dto.BrandImage;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.DefaultViewComponents
{
    public class _MainBrandSliderComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultBrandWithImages> _readService;
        public _MainBrandSliderComponentPartial(IReadService<ResultBrandWithImages> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brandImage = await _readService.GetAll("Brands/GetAllBrandWithBrandImage", "brandWithImages");
            return View(brandImage);
        }
    }
}
