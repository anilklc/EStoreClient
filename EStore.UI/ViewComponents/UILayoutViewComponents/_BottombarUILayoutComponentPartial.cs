using EStore.Services.Helper;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.UILayoutViewComponents
{
    public class _BottombarUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.IsUserLoggedIn = UserHelper.IsUserLoggedIn(HttpContext);
            ViewBag.UserName = UserHelper.GetUserName(HttpContext);
            return View();
        }
    }
}
