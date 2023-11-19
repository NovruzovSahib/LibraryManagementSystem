using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            var categorylist = _appDbContext.Categories.ToList();
            return View(categorylist);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Categories.Add(category);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult ShowAddCategoryForm()
        {
            return View("AddCategory");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(b => b.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = await _appDbContext.Categories.FindAsync(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                existingCategory.CategoryName = updatedCategory.CategoryName;
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryid = _appDbContext.Categories.FirstOrDefault(a => a.CategoryId == id);

            if (categoryid != null)
            {
                _appDbContext.Categories.Remove(categoryid);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
