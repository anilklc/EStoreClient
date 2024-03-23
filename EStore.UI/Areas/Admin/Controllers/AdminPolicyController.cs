using EStore.Dto.Policies;
using EStore.Services;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminPolicy")]
    public class AdminPolicyController : Controller
    {
       private readonly IWriteService<CreatePolicy> _writeService;
        private readonly IReadService<ResultPolicy> _readService;

        public AdminPolicyController(IWriteService<CreatePolicy> writeService, IReadService<ResultPolicy> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var policies = await _readService.GetAll("Policies/GetAllPolicies", "policies");
            return View(policies);
        }

        //[HttpGet]
        //[Route("[action]")]
        //public IActionResult CreatePolicy()
        //{

        //    return View();

        //}

        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> CreatePolicy(CreatePolicy createPolicy)
        //{
        //    try
        //    {
        //        var policy = await _writeService.AddAsync(createPolicy, "Policies/CreatePolicy");

        //        return RedirectToAction("Index", "AdminPolicy", new { area = "Admin" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }
        //}
    }
}
