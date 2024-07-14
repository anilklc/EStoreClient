using EStore.Dto.Address;
using EStore.Dto.Order;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserOrder")]
    public class UserOrderController : Controller
    {
        private readonly IReadService<UserOrderResult> _readService;

        public UserOrderController(IReadService<UserOrderResult> readService)
        {
            _readService = readService;
        }

        [HttpGet("Index")]
       
        public async Task<IActionResult> Index(string status)
        {
            var orders = string.IsNullOrEmpty(status) ? 
                await _readService.GetAll($"Orders/GetAllOrderByUserId/{UserHelper.GetUserName(HttpContext)}/ /", "orders") :
                await _readService.GetAll($"Orders/GetAllOrderByUserId/{UserHelper.GetUserName(HttpContext)}/{status}/", "orders");
            return View(orders);
        }
    }
}
