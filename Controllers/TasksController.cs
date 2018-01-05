using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskImpossible.Data;
using TaskImpossible.Models;
using TaskImpossible.Services;

namespace TaskImpossible.Controllers
{
    public class TasksController : Controller
    {
        private readonly ImpossibleContext _context;

        private readonly IHostingEnvironment _environment;

        public TasksController(ImpossibleContext context, IHostingEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string s, string so, string cf, int? page, double? lt, double? ln, double? r)
        {
            IQueryable<iTask> tasks;
            ViewData["cs"] = so;

            ViewData["NameSortParm"] = string.IsNullOrEmpty(so) ? "name_desc" : "";
            ViewData["DateSortParm"] = so == "Date" ? "date_desc" : "Date";
            if (lt != null) ViewData["MyLat"] = lt;
            if (ln != null) ViewData["MyLon"] = ln;
            if (r != null)
            {
                ViewData["SearchRadius"] = r;
            }
            else
            {
                ViewData["SearchRadius"] = 5;
            }

            if (s != null)
            {
                page = 1;
            }
            else
            {
                s = cf;
            }

            ViewData["cf"] = s;
            tasks = TaskFilter(_context.Tasks, s, so, lt, ln, r);
            switch (so)
            {
                case "distance":
                    tasks = tasks.OrderByDescending(t => t.Location);
                    break;
                case "name_desc":
                    tasks = tasks.OrderByDescending(t => t.Title);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(t => t.CreationDate);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(t => t.CreationDate);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Title);
                    break;
            }

            var pageSize = 12;


            string json = JsonConvert.SerializeObject(tasks);
            //ViewData["chart"] = json;
            ViewBag.TaskJson = json;
            return View(await PagedList.PaginatedList<iTask>.CreateAsync(tasks.AsNoTracking(), page ?? 1, pageSize));
        }

        private IQueryable<iTask> TaskFilter(DbSet<iTask> MasterData, string searchString, string sortOrder, double? Latitude, double? Longitude, double? searchRadius)
        {
            IQueryable<iTask> result = MasterData;

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(t => t.Title.Contains(searchString) || t.Description.Contains(searchString) || searchString==null);
            }

            if (Latitude!=null && Longitude!=null && searchRadius!=null && searchRadius>0)
            {

                result = result.Where(t => CalculateDistance(t.Lat, t.Lon, (double) Latitude, (double) Longitude) < searchRadius);
            }
           

            return result;

        }

        private double CalculateDistance(double tLat, double tLon, double latitude, double longitude)
        {
             var R = 6371; // Radius of the earth in km
                var dLat = Math.PI / 180 * (tLat - latitude); // deg2rad below
                var dLon = Math.PI / 180 * (tLon - longitude);
                var a =
                        Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                        Math.Cos(Math.PI / 180 * latitude) * Math.Cos(Math.PI / 180 * tLat) *
                        Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
                    ;
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                var d = R * c; // Distance in km
            return d;
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var iTask = await _context.Tasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (iTask == null)
                return NotFound();

            return View(iTask);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "Id,Owner,Title,Description,Category,Type,Location,Lat,Lon,Radius,NegotiationMarker,Status,CreationDate,StartDate,EndDate,Renumeration,PaymentTerms,Currency,CoverPhoto,DatePretext")]
            iTask iTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(iTask);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var iTask = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            if (iTask == null)
                return NotFound();
            return View(iTask);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind(
                "Id,Owner,Title,Description,Category,Type,Location,Lat,Lon,Radius,NegotiationMarker,Status,CreationDate,StartDate,EndDate,Renumeration,PaymentTerms,Currency,CoverPhoto,DatePretext")]
            iTask iTask)
        {
            if (id != iTask.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!iTaskExists(iTask.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(iTask);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var iTask = await _context.Tasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (iTask == null)
                return NotFound();

            return View(iTask);
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iTask = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tasks.Remove(iTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool iTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }


        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "Uploads");
            var newFilename = DateTime.Now.DayOfYear + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second +
                              file.FileName;
            if (file.Length > 0)
                using (var fileStream = new FileStream(Path.Combine(uploads, newFilename), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }


            return Json("/Uploads/" + newFilename);
        }
    }
}