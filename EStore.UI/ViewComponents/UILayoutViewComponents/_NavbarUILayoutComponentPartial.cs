using EStore.Dto.Announcement;
using EStore.Dto.Category;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultAnnouncement> _readService;
        public _NavbarUILayoutComponentPartial(IReadService<ResultAnnouncement> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var announcements = await _readService.GetAll("Announcements/GetAllAnnouncements", "announcements");
            return View(announcements);
        }
    }
}
