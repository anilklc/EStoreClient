using EStore.Dto.Address;
using EStore.Dto.Address;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserAddress")]

    public class UserAddressController : Controller
    {
        private readonly IWriteService<CreateAddress, UpdateAddress> _writeService;
        private readonly IReadService<ResultAddress> _readService;

        public UserAddressController(IWriteService<CreateAddress, UpdateAddress> writeService, IReadService<ResultAddress> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var addresss = await _readService.GetAll($"Addresses/GetAddressByUsername/{UserHelper.GetUserName(HttpContext)}", "address");
            return View(addresss);
        }

        [HttpGet("[action]")]
        public IActionResult CreateAddress()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAddress(CreateAddress createAddress)
        {
            createAddress.UserName = UserHelper.GetUserName(HttpContext);
            var result = await _writeService.AddAsync(createAddress, "Addresses/CreateAddress");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveAddress(string id)
        {
            await _writeService.DeleteAsync(id, "Addresses/RemoveAddress/");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateAddress(string id)
        {
            var address = await _readService.Get("Addresses/GetByIdAddress/", id);
            return View(address);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateAddress(UpdateAddress updateAddress)
        {
            var result = await _writeService.UpdateAsync(updateAddress, "Addresses/UpdateAddress/");
            return RedirectToAction(nameof(Index));
        }
    }
}
