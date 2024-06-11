using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.ProductDetailViewComponents
{
    public class _RelatedProductComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
