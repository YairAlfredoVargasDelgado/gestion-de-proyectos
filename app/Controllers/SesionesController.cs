using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using System.Collections.Generic;

namespace App.Controllers
{
    public class SesionesController : Controller
    {
        private readonly ContextoApp _context;

        public SesionesController(ContextoApp context)
        {
            _context = context;
        }

        // GET: Sesiones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sesión.ToListAsync());
        }

        // GET: Sesiones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesión = await _context.Sesión
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesión == null)
            {
                return NotFound();
            }

            return View(sesión);
        }

        // GET: Sesiones/Create
        public IActionResult Create()
        {
            ViewBag.MensajeDeError = "";

            return View();
        }

        // POST: Sesiones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Estado,Nombre,Contraseña")] Sesión sesión)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = (from _usuario in _context.Usuario
                                    where _usuario.Nombre == sesión.Nombre
                                    select _usuario).FirstOrDefault();
                
                if (usuario == null)
                {
                    ViewBag.MensajeDeError = "El usuario no está registrado";
                    return View(sesión);
                }

                if (usuario.Contraseña != sesión.Contraseña)
                {
                    ViewBag.MensajeDeError = "La contraseña es incorrecta";
                    return View(sesión);
                }

                sesión.Fecha = DateTime.Now;
                sesión.Estado = SesiónState.INICIADA;
                sesión.Usuario = usuario;

                if (usuario.Rol == Rol.CALIFICADOR)
                {
                    IList<Proyecto> proyectos = (from p in _context.Proyecto
                                                where p.Calificador1.Id == usuario.Id ||
                                                    p.Calificador2.Id == usuario.Id
                                                select p).ToList();
                    ((Calificador)usuario).Proyectos = proyectos;
                }

                if (usuario.Rol == Rol.DIRECTOR)
                {
                    IList<Proyecto> proyectos = (from p in _context.Proyecto
                                                where p.Director.Id == usuario.Id
                                                select p).ToList();
                    ((Director)usuario).Proyectos = proyectos;
                }

                _context.Add(sesión);

                usuario.Sesiones.Add(sesión);
                _context.Update(usuario);
                
                await _context.SaveChangesAsync();

                SesiónActual.Sesión = sesión;

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(sesión);
        }

        // GET: Sesiones/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesión = await _context.Sesión.FindAsync(id);
            if (sesión == null)
            {
                return NotFound();
            }
            return View(sesión);
        }

        // POST: Sesiones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Fecha,Estado")] Sesión sesión)
        {
            if (id != sesión.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesión);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesiónExists(sesión.Id))
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
            return View(sesión);
        }

        public async Task<IActionResult> CerrarSesión()
        {
            return RedirectToAction(nameof(Delete), new { id = SesiónActual.Sesión.Id });
        }

        // GET: Sesiones/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesión = await _context.Sesión
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesión == null)
            {
                return NotFound();
            }

            return View(sesión);
        }

        // POST: Sesiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sesión = await _context.Sesión.FindAsync(id);
            sesión.Estado = SesiónState.CERRADA;
            _context.Sesión.Update(sesión);
            //_context.Sesión.Remove(sesión);
            await _context.SaveChangesAsync();
            SesiónActual.Sesión = null;

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        private bool SesiónExists(long id)
        {
            return _context.Sesión.Any(e => e.Id == id);
        }
    }
}
