using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.ProductViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
