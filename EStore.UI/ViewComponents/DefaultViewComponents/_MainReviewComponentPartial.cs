using EStore.Dto.Review;
using EStore.Dto.ReviewImage;
using EStore.Dto.Slider;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.DefaultViewComponents
{
    public class _MainReviewComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultReviewWithImages> _readService;
        public _MainReviewComponentPartial(IReadService<ResultReviewWithImages> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sldierImage = await _readService.GetAll("Reviews/GetAllReviewWithReviewImage", "reviewWithReviewImages");
            return View(sldierImage);
        }
    }
}