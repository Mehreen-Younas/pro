using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ViewData["PastaChef"] = await _context.PastaChef.ToListAsync();
            ViewData["Chefs"] = await _context.Chef.ToListAsync();
            var applicationDbContext = _context.Pasta.Include(b => b.Shapes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasta = await _context.Pasta
                .Include(b => b.Shapes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasta == null)
            {
                return NotFound();
            }

            return View(pasta);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["ShapesId"] = new SelectList(_context.Shape, "Id", "Name");
            ViewData["ChefId"] = new SelectList(_context.Chef, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,URL,Price,ShapesId")] Pasta pasta, List<int> Chefs)
        {

            if (ModelState.IsValid)
            {
                _context.Pasta.Add(pasta);
                await _context.SaveChangesAsync();
                List<PastaChef> pastaChef = new List<PastaChef>();
                foreach (int chef in Chefs)
                {
                    pastaChef.Add(new PastaChef { ChefId = chef, PastaId = pasta.Id });
                }
                _context.PastaChef.AddRange(pastaChef);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ShapesId"] = new SelectList(_context.Shape, "Id", "Id", pasta.ShapesId);
            return View(pasta);
        }

        // GET: Books/Edit/5
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

            IList<PastaChef> pastaChefs = await _context.PastaChef.Where<PastaChef>(a => a.PastaId == pasta.Id).ToListAsync();
            IList<int> listChefs = new List<int>();
            foreach (PastaChef pastaChef in pastaChefs)
            {
                listChefs.Add(pastaChef.ChefId);
            }
            // var authors = await _context.Author.Where(a=>a.Id.Equals(listAuthors)).ToListAsync();



            ViewData["ShapesId"] = new SelectList(_context.Shape, "Id", "Name", pasta.ShapesId);
            ViewData["ChefId"] = new MultiSelectList(_context.Chef, "Id", "Name", listChefs.ToArray());
            return View(pasta);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,URL,Price,ShapesId")] Pasta pasta)
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
            ViewData["ShapesId"] = new SelectList(_context.Shape, "Id", "Id", pasta.ShapesId);
            return View(pasta);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasta = await _context.Pasta
                .Include(b => b.Shapes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasta == null)
            {
                return NotFound();
            }

            return View(pasta);
        }

        // POST: Books/Delete/5
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
