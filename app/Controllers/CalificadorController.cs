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
    public class CalificadorController : Controller
    {
        private readonly ContextoApp _context;

        public CalificadorController(ContextoApp context)
        {
            _context = context;
        }

        // GET: Calificador
        public async Task<IActionResult> Index()
        {
            return View(await _context.Calificador.ToListAsync());
        }

        // GET: Calificador/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificador = await _context.Calificador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificador == null)
            {
                return NotFound();
            }

            var proyectos = (from p in _context.Proyecto
                            where p.Calificador1.Id == id ||
                                  p.Calificador2.Id == id
                            select p).ToList();
            
            calificador.Proyectos = proyectos;

            return View(calificador);
        }

        public async Task<IActionResult> Calificar(long id)
        {
            Console.WriteLine(id);
            Proyecto p = await (from _p in _context.Proyecto
                                where _p.Id == id
                            select _p)
                            .Include("Asignatura")
                            .Include("Calificador1")
                            .Include("Calificador2")
                            .Include("Director")
                            .Include(__p => __p.Rúbrica)
                                .ThenInclude(c => c.Criterios)
                            .FirstOrDefaultAsync();
            Console.WriteLine(p.Rúbrica.Nombre);
            foreach(var c in p.Rúbrica.Criterios)
            {
                Console.WriteLine(c.Descripción);
            }
            if (p != null)
            {
                return View(p);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Calificador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calificador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorreoElectrónico,Nombre,Contraseña,Identificación,Sexo,Edad,Nombres,PrimerApellido,SegundoApellido,Id")] Calificador calificador)
        {
            if (ModelState.IsValid)
            {
                calificador.Rol = Rol.CALIFICADOR;
                _context.Add(calificador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calificador);
        }

        // GET: Calificador/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificador = await _context.Calificador.FindAsync(id);
            if (calificador == null)
            {
                return NotFound();
            }
            return View(calificador);
        }

        // POST: Calificador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CorreoElectrónico,Contraseña,Identificación,Sexo,Edad,Nombres,PrimerApellido,SegundoApellido,Id")] Calificador calificador)
        {
            if (id != calificador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificadorExists(calificador.Id))
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
            return View(calificador);
        }

        // GET: Calificador/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificador = await _context.Calificador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificador == null)
            {
                return NotFound();
            }

            return View(calificador);
        }

        // POST: Calificador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var calificador = await _context.Calificador.FindAsync(id);
            _context.Calificador.Remove(calificador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificadorExists(long id)
        {
            return _context.Calificador.Any(e => e.Id == id);
        }
    }
}
