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
    public class RevendeursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RevendeursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Revendeurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Revendeurs.ToListAsync());
        }

        // GET: Revendeurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendeur = await _context.Revendeurs
                .FirstOrDefaultAsync(m => m.RevendeurID == id);
            if (revendeur == null)
            {
                return NotFound();
            }

            return View(revendeur);
        }

        // GET: Revendeurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Revendeurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RevendeurID,TypeRevendeur,NomRevendeur")] Revendeur revendeur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revendeur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(revendeur);
        }

        // GET: Revendeurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendeur = await _context.Revendeurs.FindAsync(id);
            if (revendeur == null)
            {
                return NotFound();
            }
            return View(revendeur);
        }

        // POST: Revendeurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RevendeurID,TypeRevendeur,NomRevendeur")] Revendeur revendeur)
        {
            if (id != revendeur.RevendeurID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revendeur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevendeurExists(revendeur.RevendeurID))
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
            return View(revendeur);
        }

        // GET: Revendeurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revendeur = await _context.Revendeurs
                .FirstOrDefaultAsync(m => m.RevendeurID == id);
            if (revendeur == null)
            {
                return NotFound();
            }

            return View(revendeur);
        }

        // POST: Revendeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revendeur = await _context.Revendeurs.FindAsync(id);
            _context.Revendeurs.Remove(revendeur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevendeurExists(int id)
        {
            return _context.Revendeurs.Any(e => e.RevendeurID == id);
        }
    }
}
