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
    public class PosteTravailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PosteTravailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PosteTravails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PosteTravails.Include(p => p.Employe);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PosteTravails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posteTravail = await _context.PosteTravails
                .Include(p => p.Employe)
                .FirstOrDefaultAsync(m => m.NumPoste == id);
            if (posteTravail == null)
            {
                return NotFound();
            }

            return View(posteTravail);
        }

        // GET: PosteTravails/Create
        public IActionResult Create()
        {
            ViewData["EmployeID"] = new SelectList(_context.Employes, "EmployeID", "AdrCourriel");
            return View();
        }

        // POST: PosteTravails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumPoste,TypePoste,EmployeID")] PosteTravail posteTravail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posteTravail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeID"] = new SelectList(_context.Employes, "EmployeID", "AdrCourriel", posteTravail.EmployeID);
            return View(posteTravail);
        }

        // GET: PosteTravails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posteTravail = await _context.PosteTravails.FindAsync(id);
            if (posteTravail == null)
            {
                return NotFound();
            }
            ViewData["EmployeID"] = new SelectList(_context.Employes, "EmployeID", "AdrCourriel", posteTravail.EmployeID);
            return View(posteTravail);
        }

        // POST: PosteTravails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumPoste,TypePoste,EmployeID")] PosteTravail posteTravail)
        {
            if (id != posteTravail.NumPoste)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posteTravail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosteTravailExists(posteTravail.NumPoste))
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
            ViewData["EmployeID"] = new SelectList(_context.Employes, "EmployeID", "AdrCourriel", posteTravail.EmployeID);
            return View(posteTravail);
        }

        // GET: PosteTravails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posteTravail = await _context.PosteTravails
                .Include(p => p.Employe)
                .FirstOrDefaultAsync(m => m.NumPoste == id);
            if (posteTravail == null)
            {
                return NotFound();
            }

            return View(posteTravail);
        }

        // POST: PosteTravails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var posteTravail = await _context.PosteTravails.FindAsync(id);
            _context.PosteTravails.Remove(posteTravail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosteTravailExists(string id)
        {
            return _context.PosteTravails.Any(e => e.NumPoste == id);
        }
    }
}
