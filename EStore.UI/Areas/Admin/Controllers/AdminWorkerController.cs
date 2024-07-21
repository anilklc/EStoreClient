using EStore.Dto.Role;
using EStore.Dto.User;
using EStore.Dto.Worker;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using EStore.Services.Services;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Configuration;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminWorker")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminCookie")]
    public class AdminWorkerController : BaseController
    {
        private readonly IWriteService<object, UpdateUserRole> _writeService;
        private readonly IReadService<ResultUser> _readService;
        private readonly IReadService<ResultRole> _resultUserRole;
        public AdminWorkerController(IWriteService<object, UpdateUserRole> writeService, IReadService<ResultUser> readService, IReadService<ResultRole> resultUserRole)
        {
            _writeService = writeService;
            _readService = readService;
            _resultUserRole = resultUserRole;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var Users = await _readService.GetAll("Users/GetAllUsers", "users");
            return View(Users);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> CreateUser()
        {
            var role = await _resultUserRole.GetAll("Roles/GetAllRole", "roles");
            List<SelectListItem> roleValues = (from x in role
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.Name,
                                                }).ToList();
            ViewBag.RoleValues = roleValues;
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUser createUser)
        {
            createUser.AuthorizedRole = UserHelper.GetUserRole(HttpContext);
            createUser.Authorized = UserHelper.GetUserName(HttpContext);
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createUser, "Users/CreateUserAdmin"), "User creation successful.", "User creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveUser(string id)
        {
            string authorizedRole = UserHelper.GetUserRole(HttpContext);
            string authorized = UserHelper.GetUserName(HttpContext);
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync($"{id}/{authorizedRole}/{authorized}", "Users/RemoveUser/"), "User removal successful.", "User removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await _readService.Get("Users/GetUserByUserId/", id);
            var role = await _resultUserRole.GetAll("Roles/GetAllRole", "roles");
            List<SelectListItem> roleValues = (from x in role
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Name,
                                               }).ToList();
            ViewBag.RoleValues = roleValues;
            var updateUser = new UpdateUserRole
            {
                Id = user.Id,
                Username = user.Username,
            };

            return View(updateUser);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserRole updateUserRole)
        {
            updateUserRole.Authorized = UserHelper.GetUserName(HttpContext);
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(updateUserRole, "Roles/AddRoleUser/"), "User update successful.", "User update failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateUserPassword(string id)
        {
            var user = await _readService.Get("Users/GetUserByUserId/", id);
            var updateUserPassword = new UpdateUserPassword
            {
                Username = user.Username,
            };
            return View(updateUserPassword);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPassword updateUserPassword)
        {
            updateUserPassword.Authorized = UserHelper.GetUserName(HttpContext);
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(updateUserPassword, "Users/UpdatePassword"), "User update successful.", "User update failed.");
            return RedirectToAction(nameof(Index));
        }

    }
}
