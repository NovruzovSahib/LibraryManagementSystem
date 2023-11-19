using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;

public class FileUploadController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(FileUploadModel model)
    {
        if (model.File != null && model.File.Length > 0)
        {
            var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var filePath = Path.Combine(uploadsDirectory, model.File.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(fileStream);
            }

            ViewBag.Message = "File uploaded successfully!";
            ViewBag.ImagePath = "/uploads/" + model.File.FileName; // Relative path to access the image in the view
        }
        else
        {
            ViewBag.Message = "No file selected!";
        }

        return View();
    }
}
