using EStore.Dto.Category;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminCategory")]
    [Authorize(Roles = "Admin,Editor")]

    public class AdminCategoryController : BaseController
    {
        private readonly IWriteService<CreateCategory, UpdateCategory> _writeService;
        private readonly IReadService<ResultCategory> _readService;

        public AdminCategoryController(IWriteService<CreateCategory, UpdateCategory> writeService, IReadService<ResultCategory> readService)
        {
            _writeService = writeService;
            _readService = readService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var categories = await _readService.GetAll("Categories/GetAllCategories", "categories");
            return View(categories);
        }

        [HttpGet("[action]")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCategory(CreateCategory createCategory)
         {
            var result = await HandleServiceAction(async () => await _writeService.AddAsync(createCategory, "Categories/CreateCategory"), "Category creation successful.", "Category creation failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> RemoveCategory(string id)
        {
            var result = await HandleServiceAction(async () => await _writeService.DeleteAsync(id, "Categories/RemoveCategory/"), "Category removal successful.", "Category removal failed.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var Category = await _readService.Get("Categories/GetByIdCategory/", id);
            return View(Category);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategory updateCategory)
        {
            var result = await HandleServiceAction(async () => await _writeService.UpdateAsync(updateCategory, "Categories/UpdateCategory/"), "Category update successful.", "Category update failed.");
            return RedirectToAction(nameof(Index));
        }
    }
}
