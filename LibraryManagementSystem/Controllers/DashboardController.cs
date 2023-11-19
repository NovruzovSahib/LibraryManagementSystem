using LibraryManagementSystem.Data;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public DashboardController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            var totalbooks = _appDbContext.Books.Count();
            var totalauthors = _appDbContext.Authors.Count();
            var totalcategories = _appDbContext.Categories.Count();
            var totalreader = _appDbContext.Readers.Count();
            var totalworker = _appDbContext.Workers.Count();


            var viewmodel = new DashboardViewModel
            {
                TotalBooks = totalbooks,
                Authors = totalauthors,
                Categories = totalcategories,
                Readers = totalreader,
                Workers = totalworker
            };
            return View(viewmodel);
        }
    }
}
