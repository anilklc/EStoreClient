using EStore.Dto.Address;
using EStore.Dto.Cart;
using EStore.Dto.Order;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Security.Principal;

namespace EStore.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IReadService<ResultAddress> _readService;
        private readonly IWriteService<CreateOrder, CreateOrder> _writeService;

        public CartController(IReadService<ResultAddress> readService, IWriteService<CreateOrder, CreateOrder> writeService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            if (!String.IsNullOrEmpty(UserHelper.GetUserName(HttpContext)))
            {
                var addresses = await _readService.GetAll($"Addresses/GetAddressByUsername/{UserHelper.GetUserName(HttpContext)}", "address");
                List<SelectListItem> addressValues = (from x in addresses
                                                   select new SelectListItem
                                                   {
                                                       Text = x.AddressTitle,
                                                       Value = x.Id
                                                   }).ToList();

                ViewBag.addressValues = addressValues;
                return View();
            }

            return RedirectToAction("Index", "Default");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] Dictionary<string, object> postData)
        {
            if (postData == null || !postData.ContainsKey("addressId") || !postData.ContainsKey("cartData") ||
               (!postData["cartData"].ToString().Contains("cartItems")))
            {
                return BadRequest(new { redirectUrl = Url.Action("Index", "Products") });
            }

            string addressId = postData["addressId"].ToString();
            var cartData = JsonConvert.DeserializeObject<Dictionary<string, object>>(postData["cartData"].ToString());

            CreateOrder createOrder = new CreateOrder
            {
                UserName = UserHelper.GetUserName(HttpContext),
                AddressId = addressId,
                CargoTracking = "",
                OrderStatus = "Order Placed",
                TotalPrice = Convert.ToDecimal(cartData["grandTotal"]),
                OrderDetails = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(cartData["cartItems"].ToString())
                                .Select(item => new OrderDetail
                                {
                                    ProductId = item.ContainsKey("productId") ? item["productId"].ToString() : "",
                                    ProductName = item["productName"].ToString() + " - Size:" + item["size"].ToString(),
                                    ProductPrice = Convert.ToDecimal(item["productPrice"]),
                                    ProductAmount = Convert.ToInt32(item["quantity"]),
                                    ProductTotalPrice = Convert.ToDecimal(item["productPrice"]) * Convert.ToInt32(item["quantity"])
                                }).ToList()
            };

            if (createOrder != null)
            {
                await _writeService.AddAsync(createOrder, "Orders/CreateOrder");
                return Ok(new { redirectUrl = Url.Action("Index", "UserOrder", new { area = "Account" }) });
            }
            else
            {
                return Ok(new { redirectUrl = Url.Action("Index", "Cart") });
            }
        }

        [HttpPost]
        public IActionResult ClearCart()
        {

            return Ok(new { success = true });
        }

       
    }
}

