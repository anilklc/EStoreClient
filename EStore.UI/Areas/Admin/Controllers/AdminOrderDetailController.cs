using EStore.Dto.Order;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminOrderDetail")]
    [Authorize(Roles = "Admin,Editor")]
    public class AdminOrderDetailController : BaseController
    {

        private readonly IReadService<OrderDetail> _readService;
        public AdminOrderDetailController(IReadService<OrderDetail> readService)
        {
            _readService = readService;
        }

        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var orderDetails = await _readService.GetAll($"OrderDetails/GetAllOrderByOrderId/{id}/", "orderDetails");
            return View(orderDetails);
        }
    }
}
