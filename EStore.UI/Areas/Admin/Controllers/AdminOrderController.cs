using EStore.Dto.Order;
using EStore.Dto.Order;
using EStore.Dto.OrderStatus;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminOrder")]
    [Authorize(Roles = "Admin,Editor", AuthenticationSchemes = "AdminCookie")]
    public class AdminOrderController : BaseController
    {
        private readonly IWriteService<UpdateOrderCargo, object> _writeService;
        private readonly IReadService<ResultOrder> _readService;

        public AdminOrderController(IWriteService<UpdateOrderCargo, object> writeService, IReadService<ResultOrder> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var orders = await _readService.GetAll("Orders/GetAllOrder/ /", "orders");
            return View(orders);
        }

        [HttpGet("[action]/{status}")]
        public async Task<IActionResult> Index(string status)
        {
            var orders = string.IsNullOrEmpty(status) ?
               await _readService.GetAll($"Orders/GetAllOrder/ /", "orders") :
               await _readService.GetAll($"Orders/GetAllOrder/{status}/", "orders");
            return View(orders);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult UpdateOrderStatus(string id)
        {
            OrderStatusList orderStatusList = new();

            List<SelectListItem> statusValues = orderStatusList.Status.Select(s => new SelectListItem
            {
                Text = s,
                Value = s
            }).ToList();
            ViewBag.StatusValues = statusValues;
            return View();
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatus updateOrderStatus)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateOrderStatus, "Orders/UpdateOrderStatus/"), "Order Status update successful.", "Order Status update failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public IActionResult UpdateOrderCargo(string id)
        {
            return View();
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateOrderCargo(UpdateOrderCargo updateOrderCargo)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateOrderCargo, "Orders/UpdateOrderCargo/"), "Order Cargo update successful.", "Order Cargo update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
