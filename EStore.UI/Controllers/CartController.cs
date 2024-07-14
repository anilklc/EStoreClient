using EStore.Dto.Cart;
using EStore.Dto.Order;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout([FromBody] string cartItem)
        {
            if (cartItem != null)
            {
                // model içinde gridValue adında bir property olacak
                string prductName = cartItem;

                // Burada alınan değeri işleyebilirsiniz
                // Örneğin, başka bir işlem yapabilir veya veritabanına kaydedebilirsiniz

                return Ok(new { message = "Veri başarıyla alındı" });
            }

            return BadRequest(new { message = "Geçersiz istek" });
        }

        [HttpPost]
        public IActionResult ClearCart()
        {

            return Ok(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> AddItems([FromBody] Dictionary<string, object> cartData)
        {
            if (cartData != null && cartData.Count > 0)
            {
                CreateOrder createOrder = new CreateOrder();
                createOrder.CartTotal = cartData["cartTotal"].ToString();
                createOrder.GrandTotal = cartData["grandTotal"].ToString();

                List<CartItemList> cartItems = new List<CartItemList>();

                // cartData["cartItems"]'ı JSON string olarak alıp, List<Dictionary<string, object>> türüne dönüştürüyoruz
                string cartItemsJson = cartData["cartItems"].ToString();
                List<Dictionary<string, object>> cartItemsData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(cartItemsJson);

                foreach (var item in cartItemsData)
                {
                    CartItemList cartItem = new CartItemList
                    {
                        ProductId = item.ContainsKey("productId") ? item["productId"].ToString() : "",
                        ProductName = item["productName"].ToString()+ " - Size:" + item["size"].ToString(),
                        ProductCoverImage = item["productCoverImage"].ToString(),
                        ProductPrice = Convert.ToDecimal(item["productPrice"]),
                        ProductAmount = Convert.ToInt32(item["quantity"]),
                        ProductTotal = Convert.ToDecimal(item["productPrice"]) * Convert.ToInt32(item["quantity"]),
                    };

                    cartItems.Add(cartItem);
                }

                createOrder.CartItems = cartItems;

                return Ok(new { message = "Veriler başarıyla alındı ve işlendi", order = createOrder });
            }

            return BadRequest(new { message = "Geçersiz istek veya veri yok" });
        }

    }
}

