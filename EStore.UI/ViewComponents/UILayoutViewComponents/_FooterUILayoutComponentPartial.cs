using EStore.Dto.Footer;
using EStore.Dto.SocialMedia;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultFooter> _readService;
        public _FooterUILayoutComponentPartial(IReadService<ResultFooter> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footer = await _readService.GetAll("Footers/GetAllFooters", "footer");
            return View(footer);
        }
    }
}
