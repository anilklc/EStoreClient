using EStore.Dto.Category;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.DefaultViewComponents
{
    public class _MainContainerCategoryComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultCategory> _readService;
        public _MainContainerCategoryComponentPartial(IReadService<ResultCategory> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sldierImage = await _readService.GetAll("Categories/GetAllPopularCategory", "categories");
            return View(sldierImage);
        }
    }
}
