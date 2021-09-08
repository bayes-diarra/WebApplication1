using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models.CustomIdentity;

namespace WebApplication1.Controllers.CustomIdentity
{
    public class WebApplication1UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebApplication1UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyApplicationRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
            //this.Users = userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToList();
        }

        // GET: MyApplicationRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webApplication1User = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webApplication1User == null)
            {
                return NotFound();
            }

            return View(webApplication1User);
        }

        // GET: MyApplicationRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyApplicationRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,EmailConfirmed,PhoneNumber,AccessFailedCount")] WebApplication1User webApplication1User)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webApplication1User);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webApplication1User);
        }

        // GET: MyApplicationRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webApplication1User = await _context.Users.FindAsync(id);
            if (webApplication1User == null)
            {
                return NotFound();
            }
            return View(webApplication1User);
        }

        // POST: MyApplicationRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,EmailConfirmed,PhoneNumber,AccessFailedCount")] WebApplication1User webApplication1User)
        {
            if (id != webApplication1User.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webApplication1User);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebApplication1UserExists(webApplication1User.Id))
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
            return View(webApplication1User);
        }

        // GET: MyApplicationRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webApplication1User = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webApplication1User == null)
            {
                return NotFound();
            }

            return View(webApplication1User);
        }

        // POST: MyApplicationRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var webApplication1User = await _context.Users.FindAsync(id);
            _context.Users.Remove(webApplication1User);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebApplication1UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
