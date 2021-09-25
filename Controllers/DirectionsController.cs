using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DirectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DirectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Directions
        public async Task<IActionResult> Index(string sortOrder,
     string currentFilter,
     string searchString,
     int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var directions = from s in _context.Directions
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                directions = directions.Where(s => s.AcronimeDirection.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    directions = directions.OrderByDescending(s => s.AcronimeDirection);
                    break;
                
                default:
                    directions = directions.OrderBy(s => s.AcronimeDirection);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Direction>.CreateAsync(directions.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Directions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _context.Directions
                .FirstOrDefaultAsync(m => m.NumDirection == id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // GET: Directions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumDirection,AncienNomDirection,NomDirection,AcronimeDirection")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(direction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(direction);
        }

        // GET: Directions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _context.Directions.FindAsync(id);
            if (direction == null)
            {
                return NotFound();
            }
            return View(direction);
        }

        // POST: Directions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumDirection,AncienNomDirection,NomDirection,AcronimeDirection")] Direction direction)
        {
            if (id != direction.NumDirection)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectionExists(direction.NumDirection))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(direction);
        }

        // GET: Directions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _context.Directions
                .FirstOrDefaultAsync(m => m.NumDirection == id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // POST: Directions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direction = await _context.Directions.FindAsync(id);
            _context.Directions.Remove(direction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectionExists(int id)
        {
            return _context.Directions.Any(e => e.NumDirection == id);
        }
    }
}
