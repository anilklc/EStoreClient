using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.UILayoutViewComponents
{
    public class _BottombarUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
