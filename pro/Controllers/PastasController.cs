using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pro.Data;
using pro.Models;

namespace pro.Controllers
{
    public class PastasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PastasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pastas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pasta.ToListAsync());
        }
        public IActionResult SearchForm() 
        {
            return View();
        }
        public async Task<IActionResult> SearchResult(string Name)
        {
            return View("Index",await _context.Pasta.Where(a=>a.Name.Contains(Name)).ToListAsync());
        }
        // GET: Pastas/Details/5
        public async Task<IActionResult> PastaItems()
        {
            return View(await _context.Pasta.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasta = await _context.Pasta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasta == null)
            {
                return NotFound();
            }

            return View(pasta);
        }

        // GET: Pastas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pastas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,URL")] Pasta pasta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pasta);
        }

        // GET: Pastas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasta = await _context.Pasta.FindAsync(id);
            if (pasta == null)
            {
                return NotFound();
            }
            return View(pasta);
        }

        // POST: Pastas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,URL")] Pasta pasta)
        {
            if (id != pasta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PastaExists(pasta.Id))
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
            return View(pasta);
        }

        // GET: Pastas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasta = await _context.Pasta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasta == null)
            {
                return NotFound();
            }

            return View(pasta);
        }

        // POST: Pastas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasta = await _context.Pasta.FindAsync(id);
            _context.Pasta.Remove(pasta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PastaExists(int id)
        {
            return _context.Pasta.Any(e => e.Id == id);
        }
    }
}
