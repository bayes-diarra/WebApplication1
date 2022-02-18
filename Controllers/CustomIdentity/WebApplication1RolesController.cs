using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.CustomIdentity;

namespace WebApplication1.Controllers.CustomIdentity
{
    public class WebApplication1RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<WebApplication1Role> _roleManager;

        public WebApplication1RolesController(ApplicationDbContext context, RoleManager<WebApplication1Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: webApplication1Roles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: webApplication1Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webApplication1Role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webApplication1Role == null)
            {
                return NotFound();
            }

            return View(webApplication1Role);
        }

        // GET: webApplication1Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: webApplication1Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] WebApplication1Role webApplication1Role)
        {
            
            if (ModelState.IsValid)
            {
                var result =  await _roleManager.CreateAsync(webApplication1Role);                
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(webApplication1Role);
        }

        // GET: webApplication1Roles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webApplication1Role = await _context.Roles.FindAsync(id);
            if (webApplication1Role == null)
            {
                return NotFound();
            }
            return View(webApplication1Role);
        }

        // POST: webApplication1Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description")] WebApplication1Role webApplication1Role)
        {
            if (id != webApplication1Role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roleManager.UpdateAsync(webApplication1Role);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!webApplication1RoleExists(webApplication1Role.Id))
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
            return View(webApplication1Role);
        }

        // GET: webApplication1Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webApplication1Role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webApplication1Role == null)
            {
                return NotFound();
            }

            return View(webApplication1Role);
        }

        // POST: webApplication1Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var webApplication1Role = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(webApplication1Role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool webApplication1RoleExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
