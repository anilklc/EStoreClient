using EStore.Dto.SocialMedia;
using EStore.Dto.SocialMedia;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminSocialMedia")]
    [Authorize(Roles = "Admin,Editor", AuthenticationSchemes = "AdminCookie")]
    public class AdminSocialMediaController : BaseController
    {
        private readonly IWriteService<CreateSocialMedia, UpdateSocialMedia> _writeService;
        private readonly IReadService<ResultSocialMedia> _readService;

        public AdminSocialMediaController(IWriteService<CreateSocialMedia, UpdateSocialMedia> writeService, IReadService<ResultSocialMedia> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var SocialMedias = await _readService.GetAll("SocialMedias/GetAllSocialMedias", "socialMedias");
            return View(SocialMedias);
        }

        [HttpGet("[action]")]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMedia createSocialMedia)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createSocialMedia, "SocialMedias/CreateSocialMedia"), "SocialMedia creation successful.", "SocialMedia creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveSocialMedia(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "SocialMedias/RemoveSocialMedia/"), "SocialMedia removal successful.", "SocialMedia removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(string id)
        {
            var SocialMedia = await _readService.Get("SocialMedias/GetByIdSocialMedia/", id);
            return View(SocialMedia);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMedia updateSocialMedia)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateSocialMedia, "SocialMedias/UpdateSocialMedia/"), "SocialMedia update successful.", "SocialMedia update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
