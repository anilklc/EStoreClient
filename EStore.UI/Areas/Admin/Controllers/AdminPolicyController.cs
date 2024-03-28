using EStore.Dto.Policies;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminPolicy")]
    public class AdminPolicyController : BaseController
    {
        private readonly IWriteService<CreatePolicy, UpdatePolicy> _writeService;
        private readonly IReadService<ResultPolicy> _readService;

        public AdminPolicyController(IWriteService<CreatePolicy, UpdatePolicy> writeService, IReadService<ResultPolicy> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var policies = await _readService.GetAll("Policies/GetAllPolicies", "policies");
            return View(policies);
        }

        [HttpGet("[action]")]
        public IActionResult CreatePolicy()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePolicy(CreatePolicy createPolicy)
        {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createPolicy, "Policies/CreatePolicy"), "Policy creation successful.", "Policy creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemovePolicy(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Policies/RemovePolicy/"), "Policy removal successful.", "Policy removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdatePolicy(string id)
        {
            var policy = await _readService.Get("Policies/GetByIdPolicy/", id);
            return View(policy);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdatePolicy(UpdatePolicy updatePolicy)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updatePolicy, "Policies/UpdatePolicy/"), "Policy update successful.", "Policy update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
