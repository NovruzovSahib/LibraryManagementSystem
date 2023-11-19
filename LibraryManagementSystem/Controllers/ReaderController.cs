using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class ReaderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ReaderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            var readerlist = _appDbContext.Readers.ToList();
            return View(readerlist);
        }
        [HttpGet]
        public IActionResult AddReader()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReader(Reader reader)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Readers.Add(reader);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult ShowAddReaderForm()
        {
            return View("AddReader");
        }
        [HttpGet]
        public IActionResult UpdateReader(int id)
        {
            var reader = _appDbContext.Readers.FirstOrDefault(b => b.ReaderId == id);

            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReader(int id, Reader updatedReader)
        {
            if (ModelState.IsValid)
            {
                var existingReader = await _appDbContext.Readers.FindAsync(id);
                if (existingReader == null)
                {
                    return NotFound();
                }

                existingReader.ReaderName = updatedReader.ReaderName;
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteReader(int id)
        {
            var readerid = _appDbContext.Readers.FirstOrDefault(a => a.ReaderId == id);

            if (readerid != null)
            {
                _appDbContext.Readers.Remove(readerid);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
