using EStore.Dto.SocialMedia;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.ViewComponents.UILayoutViewComponents
{
    public class _FooterSocialMediaComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultSocialMedia> _readService;
        public _FooterSocialMediaComponentPartial(IReadService<ResultSocialMedia> readService)
        {
            _readService = readService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var socialMedias = await _readService.GetAll("SocialMedias/GetAllSocialMedias", "socialMedias");
            return View(socialMedias);
        }
    }
}
