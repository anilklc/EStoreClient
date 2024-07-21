using EStore.Dto.Announcement;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminAnnouncement")]
    [Authorize(Roles = "Admin,Editor", AuthenticationSchemes = "AdminCookie")]
    public class AdminAnnouncementController : BaseController
    {
        private readonly IWriteService<CreateAnnouncement, UpdateAnnouncement> _writeService;
        private readonly IReadService<ResultAnnouncement> _readService;

        public AdminAnnouncementController(IWriteService<CreateAnnouncement, UpdateAnnouncement> writeService, IReadService<ResultAnnouncement> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var Announcements = await _readService.GetAll("Announcements/GetAllAnnouncements", "announcements");
            return View(Announcements);
        }

        [HttpGet("[action]")]
        public IActionResult CreateAnnouncement()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAnnouncement(CreateAnnouncement createAnnouncement)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createAnnouncement, "Announcements/CreateAnnouncement"), "Announcement creation successful.", "Announcement creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveAnnouncement(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Announcements/RemoveAnnouncement/"), "Announcement removal successful.", "Announcement removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateAnnouncement(string id)
        {
            var announcement = await _readService.Get("Announcements/GetByIdAnnouncement/", id);
            return View(announcement);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateAnnouncement(UpdateAnnouncement updateAnnouncement)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateAnnouncement, "Announcements/UpdateAnnouncement/"), "Announcement update successful.", "Announcement update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
