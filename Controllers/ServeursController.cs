using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ServeursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServeursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Serveurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Serveurs.ToListAsync());
        }

        // GET: Serveurs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serveur = await _context.Serveurs
                .FirstOrDefaultAsync(m => m.NumServeur == id);
            if (serveur == null)
            {
                return NotFound();
            }

            return View(serveur);
        }

        // GET: Serveurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Serveurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumServeur")] Serveur serveur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serveur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serveur);
        }

        // GET: Serveurs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serveur = await _context.Serveurs.FindAsync(id);
            if (serveur == null)
            {
                return NotFound();
            }
            return View(serveur);
        }

        // POST: Serveurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumServeur")] Serveur serveur)
        {
            if (id != serveur.NumServeur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serveur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServeurExists(serveur.NumServeur))
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
            return View(serveur);
        }

        // GET: Serveurs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serveur = await _context.Serveurs
                .FirstOrDefaultAsync(m => m.NumServeur == id);
            if (serveur == null)
            {
                return NotFound();
            }

            return View(serveur);
        }

        // POST: Serveurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var serveur = await _context.Serveurs.FindAsync(id);
            _context.Serveurs.Remove(serveur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServeurExists(string id)
        {
            return _context.Serveurs.Any(e => e.NumServeur == id);
        }
    }
}
