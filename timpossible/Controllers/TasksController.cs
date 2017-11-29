using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using timpossible.Data;
using timpossible.Models;

namespace timpossible.Controllers
{
    public class TasksController : Controller
    {
        private readonly ImpossibleContext _context;

        public TasksController(ImpossibleContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
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
                "Id,Owner,Title,Description,Category,Type,Location,Lat,Lon,Radius,NegotiationMarker,Status,CreationDate,TargetDate,ClosingDate,Renumeration,PaymentTerms,Currency,CoverPhoto")]
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
                "Id,Owner,Title,Description,Category,Type,Location,Lat,Lon,Radius,NegotiationMarker,Status,CreationDate,TargetDate,ClosingDate,Renumeration,PaymentTerms,Currency,CoverPhoto")]
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
    }
}