﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserDashboard")]
    [Authorize(Roles = "User")]
    public class UserDashboardController : Controller
    {

        [HttpGet("Index")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
