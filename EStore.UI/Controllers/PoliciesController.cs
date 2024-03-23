using System.Collections.Generic;
using System.Threading.Tasks;
using EStore.Dto.Policies;
using EStore.Services;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class PoliciesController : Controller
    {
        private readonly IReadService<ResultPolicy> _readService;

        public PoliciesController(IReadService<ResultPolicy> readService)
        {
            _readService = readService;
        }

        public async Task<IActionResult> Index()
        {
            var policies = await _readService.GetAll("Brands/GetAllBrand","fjgrk");
            return View(policies);
        }
    }
}
