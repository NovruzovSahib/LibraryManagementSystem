using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        //Index

        public IActionResult Index()
        {
            return View();
        }

        //Privacy

        public IActionResult Privacy()
        {
            return View();
        }

        //About

        public IActionResult About()
        {
            return View();
        }

        //Contact

        public IActionResult Contact()
        {
            return View();
        }
    }
}
