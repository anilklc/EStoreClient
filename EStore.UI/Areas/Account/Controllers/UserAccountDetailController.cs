﻿using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserAccountDetail")]
    public class UserAccountDetailController : Controller
    {
        [HttpGet("Index")]

        public IActionResult Index()
        {
            return View();
        }
    }
}