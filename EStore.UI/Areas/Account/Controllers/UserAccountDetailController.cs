using EStore.Dto.Address;
using EStore.Dto.User;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserAccountDetail")]
    [Authorize(Roles = "User")]
    public class UserAccountDetailController : Controller
    {
        private readonly IWriteService<PasswordUpdate, PasswordUpdate> _writeService;
        public UserAccountDetailController(IWriteService<PasswordUpdate, PasswordUpdate> writeService)
        {
            _writeService = writeService;
        }

        [HttpGet("Index")]

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Index")]
        public async Task<IActionResult> Index(PasswordUpdate passwordUpdate)
        {
            passwordUpdate.Authorized = UserHelper.GetUserName(HttpContext);
            passwordUpdate.Username = UserHelper.GetUserName(HttpContext);
            var result = await _writeService.AddAsync(passwordUpdate, "Users/UpdatePassword/");
            return RedirectToAction(nameof(Index));
        }

    }
}
