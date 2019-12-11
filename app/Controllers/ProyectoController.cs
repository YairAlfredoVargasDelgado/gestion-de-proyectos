using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Controllers
{
    [AllowAnonymous]
    public class ProyectoController : Controller
    {
        private readonly ContextoApp _context;

        public ProyectoController(ContextoApp context)
        {
            _context = context;
        }

        // GET: Proyecto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proyecto.ToListAsync());
        }

        // GET: Proyecto/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto
                .Include(p => p.Asignatura)
                .Include(p => p.Director)
                .Include(p => p.Calificador1)
                .Include(p => p.Calificador2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyecto/Create
        public IActionResult Create()
        {
            #region ViewBag
            ViewBag.Asignaturas = new SelectList(_context.Asignatura, "Código", "Nombre");
            if (_context.Asignatura.Count() == 0)
            {
                ViewBag.ErrorNoHayAsignaturasRegistradas = "No hay asignaturas registradas";
            }
            if (_context.Director.Count() == 0)
            {
                ViewBag.ErrorConDirector = "No hay directores registrados";
            }
            #endregion
            return View();
        }
        
        // POST: Proyecto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Código,Nombre,Id,Descripción,IdAsignatura,IdDirector")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                #region ViewBag
                ViewBag.Asignaturas = new SelectList(_context.Asignatura, "Código", "Nombre");
                if (_context.Asignatura.Count() == 0)
                {
                    ViewBag.ErrorNoHayAsignaturasRegistradas = "No hay asignaturas registradas";
                    return View(proyecto);
                }
                if (_context.Director.Count() == 0)
                {
                    ViewBag.ErrorConDirector = "No hay directores registrados";
                    return View(proyecto);
                }
                Asignatura a = _context.Asignatura.Where(a => a.Código == proyecto.IdAsignatura).FirstOrDefault();
                Director d = _context.Director.Where(d => d.Identificación == proyecto.IdDirector).FirstOrDefault();
                if (d == null)
                {
                    ViewBag.ErrorConDirector = "Este director no está registrado";
                    return View(proyecto);
                }
                ViewBag.ErrorConDirector = d.NombreCompleto();
                #endregion
                proyecto.Asignatura = a;
                proyecto.Director = d;
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyecto/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            return View(proyecto);
        }

        // POST: Proyecto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Código,Nombre,Id")] Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.Id))
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
            return View(proyecto);
        }

        // GET: Proyecto/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyecto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var proyecto = await _context.Proyecto.FindAsync(id);
            _context.Proyecto.Remove(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(long id)
        {
            return _context.Proyecto.Any(e => e.Id == id);
        }
    }
}
