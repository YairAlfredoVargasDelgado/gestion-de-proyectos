using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    [AllowAnonymous]
    public class RúbricaController : Controller
    {
        private readonly ContextoApp _context;

        public RúbricaController(ContextoApp context)
        {
            _context = context;
        }

        // GET: Rúbrica
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rúbrica.ToListAsync());
        }

        // GET: Rúbrica/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rúbrica = await _context.Rúbrica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rúbrica == null)
            {
                return NotFound();
            }

            return View(rúbrica);
        }

        // GET: Rúbrica/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rúbrica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Rúbrica rúbrica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rúbrica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rúbrica);
        }

        // GET: Rúbrica/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rúbrica = await _context.Rúbrica.FindAsync(id);
            if (rúbrica == null)
            {
                return NotFound();
            }
            return View(rúbrica);
        }

        // POST: Rúbrica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id")] Rúbrica rúbrica)
        {
            if (id != rúbrica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rúbrica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RúbricaExists(rúbrica.Id))
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
            return View(rúbrica);
        }

        // GET: Rúbrica/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rúbrica = await _context.Rúbrica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rúbrica == null)
            {
                return NotFound();
            }

            return View(rúbrica);
        }

        // POST: Rúbrica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rúbrica = await _context.Rúbrica.FindAsync(id);
            _context.Rúbrica.Remove(rúbrica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RúbricaExists(long id)
        {
            return _context.Rúbrica.Any(e => e.Id == id);
        }
    }
}
