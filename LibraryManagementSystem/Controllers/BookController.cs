using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;


namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment hostingEnvironment;
        public BookController(AppDbContext appDbContext, IWebHostEnvironment hostingEnvironment)
        {
            _appDbContext = appDbContext;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult GetBooks()
        {
            var booklist = _appDbContext.Books.Include(s=>s.Author).Include(s=>s.Category).ToList();
            return View(booklist);
        }

        // My Books
        public IActionResult GetMyBooks()
        {
            var mybooklist = _appDbContext.MyBooks.ToList();
            return View(mybooklist);
        }

        //Add Books to Library

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsDirectory))
                    {
                        Directory.CreateDirectory(uploadsDirectory);
                    }

                    var filePath = Path.Combine(uploadsDirectory, $"{book.BookId}_{file.FileName}");

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    book.ImagePath = $"/uploads/{book.BookId}_{file.FileName}";
                    await _appDbContext.SaveChangesAsync();
                }
                var category = _appDbContext.Categories.FirstOrDefault(c => c.CategoryId == book.CategoryId);
                var author = _appDbContext.Authors.FirstOrDefault(c => c.AuthorId == book.AuthorId);

                if (category != null)
                {
                    book.CategoryName = category.CategoryName;
                    book.AuthorName = author.AuthorName;
                    _appDbContext.Books.Add(book);
                    await _appDbContext.SaveChangesAsync();
                    return RedirectToAction("GetBooks");
                }
                else
                {
                    ModelState.AddModelError("CategoryId", "Invalid category selected");
                }
            }

            ViewBag.Categories = new SelectList(_appDbContext.Categories, "CategoryId", "CategoryName");
            ViewBag.Authors = new SelectList(_appDbContext.Authors, "AuthorId", "AuthorName");
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToMyBooks(int id)
        {
            var selectedBook = _appDbContext.Books.FirstOrDefault(s => s.BookId == id);

            if (selectedBook != null)
            {
                var myBook = new MyBook
                {
                    Title = selectedBook.Title,
                    Description = selectedBook.Description,
                    Language = selectedBook.Language,
                    PageCount = selectedBook.PageCount,
                    PublicationDate = selectedBook.PublicationDate,
                    CategoryName = selectedBook.CategoryName,
                    AuthorName = selectedBook.AuthorName,
                    ImagePath = selectedBook.ImagePath,
                };

                _appDbContext.MyBooks.Add(myBook);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult UpdateBook(int id)
        {
            var book = _appDbContext.Books.FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var existingBook = await _appDbContext.Books.FindAsync(id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                existingBook.Title = updatedBook.Title;
                existingBook.CategoryName = updatedBook.CategoryName;
                existingBook.AuthorName = updatedBook.AuthorName;
                existingBook.Description = updatedBook.Description;
                existingBook.Language = updatedBook.Language;
                existingBook.PageCount = updatedBook.PageCount;
                existingBook.PublicationDate = updatedBook.PublicationDate;
                if (file != null && file.Length > 0)
                {
                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsDirectory))
                    {
                        Directory.CreateDirectory(uploadsDirectory);
                    }

                    var filePath = Path.Combine(uploadsDirectory, $"{existingBook.BookId}_{file.FileName}");

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    existingBook.ImagePath = $"/uploads/{existingBook.BookId}_{file.FileName}";
                    await _appDbContext.SaveChangesAsync();
                    return RedirectToAction("GetBooks");

                }
            }
           
            return View();
        }
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookid = _appDbContext.Books.FirstOrDefault(a => a.BookId == id);

            if (bookid != null)
            {
                _appDbContext.Books.Remove(bookid);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
