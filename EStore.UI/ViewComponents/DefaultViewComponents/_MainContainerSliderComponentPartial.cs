using EStore.Dto.Slider;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.DefaultViewComponents
{
    public class _MainContainerSliderComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultSliderWithImages> _readService;
        public _MainContainerSliderComponentPartial(IReadService<ResultSliderWithImages> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sldierImage = await _readService.GetAll("SliderImages/GetAllActiveSliderWithSliderImage", "sliderWithSliderImages");
            return View(sldierImage);
        }

    }
}
