using EStore.Dto.Address;
using EStore.Dto.User;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserAccountDetail")]
    public class UserAccountDetailController : Controller
    {
        private readonly IWriteService<PasswordUpdate, PasswordUpdate> _writeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccountDetailController(IWriteService<PasswordUpdate, PasswordUpdate> writeService, IHttpContextAccessor httpContextAccessor)
        {
            _writeService = writeService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Index")]

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Index")]
        public async Task<IActionResult> Index(PasswordUpdate passwordUpdate)
        {
            passwordUpdate.Username = UserHelper.GetUserName(HttpContext);
            var result = await _writeService.AddAsync(passwordUpdate, "Users/UpdatePassword/");
            return RedirectToAction(nameof(Index));
        }

    }
}
