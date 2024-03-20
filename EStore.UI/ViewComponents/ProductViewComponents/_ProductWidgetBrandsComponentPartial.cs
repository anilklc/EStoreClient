using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.ProductViewComponents
{
    public class _ProductWidgetBrandsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}