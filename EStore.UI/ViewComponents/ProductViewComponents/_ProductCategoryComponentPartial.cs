using EStore.Dto.Category;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.ProductViewComponents
{
    public class _ProductCategoryComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultCategory> _readService;
        public _ProductCategoryComponentPartial(IReadService<ResultCategory> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _readService.GetAll("Categories/GetAllCategories", "categories");
            return View(categories);
        }
    }
}