using EStore.Dto.Address;
using EStore.Dto.Order;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserOrder")]
    [Authorize(Roles = "User", AuthenticationSchemes = "AccountCookie")]
    public class UserOrderController : Controller
    {
        private readonly IReadService<UserOrderResult> _readService;
        private readonly IReadService<OrderDetail> _orderDetailReadService;

        public UserOrderController(IReadService<UserOrderResult> readService, IReadService<OrderDetail> orderDetailReadService)
        {
            _readService = readService;
            _orderDetailReadService = orderDetailReadService;
        }

        [HttpGet("Index")]

        public async Task<IActionResult> Index()
        {
            var orders = await _readService.GetAll($"Orders/GetAllOrderByUserId/{UserHelper.GetUserName(HttpContext)}/ /", "orders");
            return View(orders);
        }

        [HttpGet("[action]/{status}")]

        public async Task<IActionResult> Index(string status)
        {
            var orders = string.IsNullOrEmpty(status) ?
                await _readService.GetAll($"Orders/GetAllOrderByUserId/{UserHelper.GetUserName(HttpContext)}/ /", "orders") :
                await _readService.GetAll($"Orders/GetAllOrderByUserId/{UserHelper.GetUserName(HttpContext)}/{status}/", "orders");
            return View(orders);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> OrderDetail(string id)
        {
            var orderDetails = await _orderDetailReadService.GetAll($"OrderDetails/GetAllOrderByOrderId/{id}/", "orderDetails");
            return View(orderDetails);
        }

    }
}
