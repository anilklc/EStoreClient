using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EStore.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout([FromBody] List<CartItem> cart)
        {
            // Sipariş işleme kodu buraya gelecek
            // Örneğin: sipariş veritabanına kaydedilecek

            // İşlemin başarılı olduğunu varsayıyoruz
            return Json(new { success = true });
        }
    }

    public class CartItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
