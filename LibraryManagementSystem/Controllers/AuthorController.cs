using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AuthorController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var authorlist = _appDbContext.Authors.ToList();
            return View(authorlist);
        }
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Authors.Add(author);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult ShowAddAuthorForm()
        {
            return View("AddAuthor");
        }

        [HttpGet]
        public IActionResult UpdateAuthor(int id)
        {
            var author = _appDbContext.Authors.FirstOrDefault(b => b.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAuthor(int id, Author updatedAuthor)
        {
            if (ModelState.IsValid)
            {
                var existingAuthor = await _appDbContext.Authors.FindAsync(id);
                if (existingAuthor == null)
                {
                    return NotFound();
                }

                existingAuthor.AuthorName = updatedAuthor.AuthorName;
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var authorid = _appDbContext.Authors.FirstOrDefault(a => a.AuthorId == id);

            if (authorid != null)
            {
                _appDbContext.Authors.Remove(authorid);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
