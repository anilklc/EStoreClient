using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.ProductViewComponents
{
    public class _ProductCategoryComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}