﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EStore.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Checkout()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            
            return Ok(new { success = true });
        }
    }
}
