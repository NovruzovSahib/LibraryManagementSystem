using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public SearchController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index(string query)
        {
            var allBooks = _appDbContext.Books.ToList();
            var searchResults = allBooks
                .Where(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return View(searchResults);
        }
    }
}
