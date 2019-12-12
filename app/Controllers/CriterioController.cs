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
    public class CriterioController : Controller
    {
        private readonly ContextoApp _context;

        public CriterioController(ContextoApp context)
        {
            _context = context;
        }

        // GET: Criterio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Criterio.ToListAsync());
        }

        // GET: Criterio/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criterio = await _context.Criterio
                .Include("Rúbrica")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criterio == null)
            {
                return NotFound();
            }

            return View(criterio);
        }

        // GET: Criterio/Create
        public IActionResult Create(long idRúbrica)
        {
            Console.WriteLine(idRúbrica);
            ViewBag.Rúbrica = (from r in _context.Rúbrica
                            where r.Id == idRúbrica
                            select r).Include("Criterios").FirstOrDefault();
            ViewBag.Rúbrica.CalcularPorcentajeRestante();
            
            ViewBag.MensajeDeError = "";
            return View();
        }

        // POST: Criterio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descripción,Id,Porcentaje,IdRúbrica")] Criterio criterio)
        {
            if (ModelState.IsValid)
            {
                Rúbrica rúbrica = (from r in _context.Rúbrica
                                    where r.Id == criterio.IdRúbrica
                                    select r)
                                    .Include("Criterios")
                                    .FirstOrDefault();
                rúbrica.CalcularPorcentajeRestante();

                if (rúbrica == null)
                {
                    ViewBag.MensajeDeError = "La rúbrica no se encuentra registrada";
                    return View(criterio);
                }

                if (rúbrica.PorcentajeRestante < criterio.Porcentaje)
                {
                    ViewBag.MensajeDeError = "La rúbrica no tiene el porcentaje restante suficiente";
                    ViewBag.Rúbrica = rúbrica;
                    return View(criterio);
                }

                criterio.Rúbrica = rúbrica;
                _context.Add(criterio);
                criterio.Rúbrica.Criterios.Add(criterio);
                _context.Update(criterio.Rúbrica);
                await _context.SaveChangesAsync();
                criterio.Rúbrica.CalcularPorcentajeRestante();
                return RedirectToAction("Details", "Rúbrica", new { id = rúbrica.Id });
            }
            return View(criterio);
        }

        // GET: Criterio/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criterio = await _context.Criterio.FindAsync(id);
            if (criterio == null)
            {
                return NotFound();
            }
            return View(criterio);
        }

        // POST: Criterio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Descripción,Porcentaje,Id")] Criterio criterio)
        {
            if (id != criterio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var criterio1 = (from c in _context.Criterio
                            where c.Id == id
                            select c).Include("Rúbrica").FirstOrDefault();
                _context.Entry(criterio1).State = EntityState.Detached;
                try
                {
                    _context.Update(criterio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriterioExists(criterio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return RedirectToAction("Details", "Rúbrica", new { id = criterio1.Rúbrica.Id });
            }
            return View(criterio);
        }

        // GET: Criterio/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criterio = await _context.Criterio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criterio == null)
            {
                return NotFound();
            }

            return View(criterio);
        }

        // POST: Criterio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var criterio = await (from c in _context.Criterio
                                    where c.Id == id
                                    select c).Include("Rúbrica").FirstOrDefaultAsync();
            long idRúbrica = criterio.Rúbrica.Id;
            _context.Criterio.Remove(criterio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Rúbrica", new { id = idRúbrica });
        }

        private bool CriterioExists(long id)
        {
            return _context.Criterio.Any(e => e.Id == id);
        }
    }
}
