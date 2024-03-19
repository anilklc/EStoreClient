using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.DefaultViewComponents
{
    public class _MainReviewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}