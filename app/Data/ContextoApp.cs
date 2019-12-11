using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Models;

    public class ContextoApp : DbContext
    {
        public ContextoApp (DbContextOptions<ContextoApp> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstudianteProyecto>().HasKey(ep => new { ep.IdEstudiante, ep.IdProyecto });
            modelBuilder.Entity<ProyectoCalificador>().HasKey(pc => new { pc.IdCalificador, pc.IdProyecto });
        }

        public DbSet<App.Models.Usuario> Usuario { get; set; }

        public DbSet<App.Models.Sesión> Sesión { get; set; }

        public DbSet<App.Models.Administrador> Administrador { get; set; }

        public DbSet<App.Models.Asignatura> Asignatura { get; set; }

        public DbSet<App.Models.Calificador> Calificador { get; set; }

        public DbSet<App.Models.Proyecto> Proyecto { get; set; }

        public DbSet<App.Models.Criterio> Criterio { get; set; }

        public DbSet<App.Models.Rúbrica> Rúbrica { get; set; }

        public DbSet<App.Models.Director> Director { get; set; }
    }
