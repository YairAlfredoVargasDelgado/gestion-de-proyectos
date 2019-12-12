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
            if (SesiónActual.Sesión.Usuario.Rol == Rol.CALIFICADOR)
            {   
                Calificador cal = (from c in _context.Calificador
                                        where c.Id == SesiónActual.Sesión.Usuario.Id
                                    select c).FirstOrDefault();
                                        
                var proyectos = (from p in _context.Proyecto
                                where p.Calificador1.Id == cal.Id ||
                                    p.Calificador2.Id == cal.Id
                                select p).ToList();
                
                Console.WriteLine(proyectos.Count);
                
                cal.Proyectos = proyectos;

                ViewBag.Calificador = cal;
            }

            if (SesiónActual.Sesión.Usuario.Rol == Rol.DIRECTOR)
            {
                Director dir = (from c in _context.Director
                                where c.Id == SesiónActual.Sesión.Usuario.Id
                                select c).FirstOrDefault();
                                        
                var proyectos = (from p in _context.Proyecto
                                where p.Calificador1.Id == dir.Id ||
                                    p.Calificador2.Id == dir.Id
                                select p).ToList();
                
                Console.WriteLine(proyectos.Count);
                
                dir.Proyectos = proyectos;

                ViewBag.Director = dir;
            }

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
                .Include("Asignatura")
                .Include("Director")
                .Include("Calificador1")
                .Include("Calificador2")
                .Include("Rúbrica")
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
            ViewBag.Asignaturas = new SelectList(_context.Asignatura, "Código", "Nombre");
            ViewBag.Rúbricas = new SelectList(_context.Rúbrica, "Id", "Nombre");

            if (_context.Asignatura.Count() == 0)
            {
                ViewBag.ErrorNoHayAsignaturasRegistradas = "No hay asignaturas registradas";
            }

            if (_context.Rúbrica.Count() == 0)
            {
                ViewBag.ErrorNoHayRúbricasRegistradas = "No hay rúbricas registradas";
            }

            if (_context.Director.Count() == 0)
            {
                ViewBag.ErrorConDirector = "No hay directores registrados";
            }

            if (_context.Calificador.Count() == 0)
            {
                ViewBag.ErrorConCalificador1 = "No hay calificadores registrados";
                ViewBag.ErrorConCalificador2 = "No hay calificadores registrados";
            }
            return View();
        }
        
        // POST: Proyecto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Código,Nombre,Id,Descripción,IdAsignatura,IdDirector,IdCalificado1,IdCalificado2,IdRúbrica")] Proyecto proyecto)
        {
            ViewBag.Asignaturas = new SelectList(_context.Asignatura, "Código", "Nombre");
            ViewBag.Rúbricas = new SelectList(_context.Rúbrica, "Id", "Nombre");

            if (ModelState.IsValid)
            {
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

                if (_context.Calificador.Count() == 0)
                {
                    ViewBag.ErrorConCalificador1 = "No hay calificadores registrados";
                    ViewBag.ErrorConCalificador2 = "No hay calificadores registrados";
                    return View(proyecto);
                }

                if (_context.Rúbrica.Count() == 0)
                {
                    ViewBag.ErrorNoHayRúbricasRegistradas = "No hay rúbricas registradas";
                    return View(proyecto);
                }

                Asignatura a = _context.Asignatura.Where(a => a.Código == proyecto.IdAsignatura).FirstOrDefault();
                Director d = _context.Director.Where(d => d.Identificación == proyecto.IdDirector).FirstOrDefault();
                Calificador c1 = _context.Calificador.Where(c => c.Identificación == proyecto.IdCalificado1).FirstOrDefault();
                Calificador c2 = _context.Calificador.Where(c => c.Identificación == proyecto.IdCalificado2).FirstOrDefault();
                Rúbrica r = _context.Rúbrica.Where(r => r.Id == proyecto.IdRúbrica).FirstOrDefault();

                if (d == null)
                {
                    ViewBag.ErrorConDirector = "Este director no está registrado";
                    return View(proyecto);
                }

                if (c1 == null)
                {
                    ViewBag.ErrorConCalificador1 = "Este calificador no está registrado";
                    return View(proyecto);
                }

                if (c2 == null)
                {
                    ViewBag.ErrorConCalificador2 = "Este calificador no está registrado";
                    return View(proyecto);
                }
                
                ViewBag.ErrorConDirector = d.NombreCompleto();

                proyecto.Asignatura = a;
                proyecto.Director = d;
                proyecto.Calificador1 = c1;
                proyecto.Calificador2 = c2;
                proyecto.Rúbrica = r;

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
