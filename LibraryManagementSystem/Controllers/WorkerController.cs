using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class WorkerController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public WorkerController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            var workerlist = _appDbContext.Workers.ToList();
            return View(workerlist);
        }
        [HttpGet]
        public IActionResult AddWorker()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWorker(Worker worker)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Workers.Add(worker);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult ShowAddWorkerForm()
        {
            return View("AddWorker");
        }
        [HttpGet]
        public IActionResult UpdateWorker(int id)
        {
            var worker = _appDbContext.Workers.FirstOrDefault(b => b.WorkerId == id);

            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateWorker(int id, Worker updatedWorker)
        {
            if (ModelState.IsValid)
            {
                var existingWorker = await _appDbContext.Workers.FindAsync(id);
                if (existingWorker == null)
                {
                    return NotFound();
                }

                existingWorker.WorkerName = updatedWorker.WorkerName;
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var workerid = _appDbContext.Workers.FirstOrDefault(a => a.WorkerId == id);

            if (workerid != null)
            {
                _appDbContext.Workers.Remove(workerid);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
