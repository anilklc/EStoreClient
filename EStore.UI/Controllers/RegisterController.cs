using EStore.Dto.Login;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IWriteService<RegisterRequest,RegisterRequest> _writeService;

        public RegisterController(IWriteService<RegisterRequest, RegisterRequest> writeService)
        {
            _writeService = writeService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _writeService.AddAsync(request, "Users/CreateUser");
            return RedirectToAction("Index", "Default");

        }
    }
}
