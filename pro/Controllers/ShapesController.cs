﻿using System;
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
    public class ShapesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShapesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shapes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shape.ToListAsync());
        }

        // GET: Shapes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shape = await _context.Shape
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shape == null)
            {
                return NotFound();
            }

            return View(shape);
        }

        // GET: Shapes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shapes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Shape shape)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shape);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shape);
        }

        // GET: Shapes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shape = await _context.Shape.FindAsync(id);
            if (shape == null)
            {
                return NotFound();
            }
            return View(shape);
        }

        // POST: Shapes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Shape shape)
        {
            if (id != shape.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shape);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShapeExists(shape.Id))
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
            return View(shape);
        }

        // GET: Shapes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shape = await _context.Shape
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shape == null)
            {
                return NotFound();
            }

            return View(shape);
        }

        // POST: Shapes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shape = await _context.Shape.FindAsync(id);
            _context.Shape.Remove(shape);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShapeExists(int id)
        {
            return _context.Shape.Any(e => e.Id == id);
        }
    }
}
