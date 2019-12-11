﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace app.Migrations
{
    [DbContext(typeof(ContextoApp))]
    partial class ContextoAppModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("App.Models.Administrador", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("App.Models.Asignatura", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CalificadorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Código")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<long?>("DirectorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CalificadorId");

                    b.HasIndex("DirectorId");

                    b.ToTable("Asignatura");
                });

            modelBuilder.Entity("App.Models.Criterio", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripción")
                        .HasColumnType("TEXT");

                    b.Property<long?>("RúbricaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RúbricaId");

                    b.ToTable("Criterio");
                });

            modelBuilder.Entity("App.Models.Estudiante", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Edad")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Identificación")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<string>("PrimerApellido")
                        .HasColumnType("TEXT");

                    b.Property<string>("SegundoApellido")
                        .HasColumnType("TEXT");

                    b.Property<int>("Sexo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Estudiante");
                });

            modelBuilder.Entity("App.Models.EstudianteProyecto", b =>
                {
                    b.Property<long>("IdEstudiante")
                        .HasColumnType("INTEGER");

                    b.Property<long>("IdProyecto")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("EstudianteId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ProyectoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdEstudiante", "IdProyecto");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("ProyectoId");

                    b.ToTable("EstudianteProyecto");
                });

            modelBuilder.Entity("App.Models.Proyecto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("AsignaturaId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("Calificador1Id")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("Calificador2Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Código")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripción")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("DirectorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AsignaturaId");

                    b.HasIndex("Calificador1Id");

                    b.HasIndex("Calificador2Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("Proyecto");
                });

            modelBuilder.Entity("App.Models.ProyectoCalificador", b =>
                {
                    b.Property<long>("IdCalificador")
                        .HasColumnType("INTEGER");

                    b.Property<long>("IdProyecto")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CalificadorId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ProyectoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdCalificador", "IdProyecto");

                    b.HasIndex("CalificadorId");

                    b.HasIndex("ProyectoId");

                    b.ToTable("ProyectoCalificador");
                });

            modelBuilder.Entity("App.Models.Rúbrica", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("AdministradorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AdministradorId");

                    b.ToTable("Rúbrica");
                });

            modelBuilder.Entity("App.Models.Sesión", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Estado")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<long?>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Sesión");
                });

            modelBuilder.Entity("App.Models.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(16);

                    b.Property<string>("CorreoElectrónico")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Edad")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Identificación")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(16);

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<string>("PrimerApellido")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rol")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SegundoApellido")
                        .HasColumnType("TEXT");

                    b.Property<int>("Sexo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("App.Models.Calificador", b =>
                {
                    b.HasBaseType("App.Models.Usuario");

                    b.HasDiscriminator().HasValue("Calificador");
                });

            modelBuilder.Entity("App.Models.Director", b =>
                {
                    b.HasBaseType("App.Models.Usuario");

                    b.HasDiscriminator().HasValue("Director");
                });

            modelBuilder.Entity("App.Models.Asignatura", b =>
                {
                    b.HasOne("App.Models.Calificador", null)
                        .WithMany("Asignaturas")
                        .HasForeignKey("CalificadorId");

                    b.HasOne("App.Models.Director", null)
                        .WithMany("Asignaturas")
                        .HasForeignKey("DirectorId");
                });

            modelBuilder.Entity("App.Models.Criterio", b =>
                {
                    b.HasOne("App.Models.Rúbrica", null)
                        .WithMany("Criterios")
                        .HasForeignKey("RúbricaId");
                });

            modelBuilder.Entity("App.Models.EstudianteProyecto", b =>
                {
                    b.HasOne("App.Models.Estudiante", "Estudiante")
                        .WithMany("EstudianteProyectos")
                        .HasForeignKey("EstudianteId");

                    b.HasOne("App.Models.Proyecto", "Proyecto")
                        .WithMany("ProyectoEstudiantes")
                        .HasForeignKey("ProyectoId");
                });

            modelBuilder.Entity("App.Models.Proyecto", b =>
                {
                    b.HasOne("App.Models.Asignatura", "Asignatura")
                        .WithMany("Proyectos")
                        .HasForeignKey("AsignaturaId");

                    b.HasOne("App.Models.Calificador", "Calificador1")
                        .WithMany()
                        .HasForeignKey("Calificador1Id");

                    b.HasOne("App.Models.Calificador", "Calificador2")
                        .WithMany()
                        .HasForeignKey("Calificador2Id");

                    b.HasOne("App.Models.Director", "Director")
                        .WithMany("Proyectos")
                        .HasForeignKey("DirectorId");
                });

            modelBuilder.Entity("App.Models.ProyectoCalificador", b =>
                {
                    b.HasOne("App.Models.Calificador", "Calificador")
                        .WithMany("CalificadorProyectos")
                        .HasForeignKey("CalificadorId");

                    b.HasOne("App.Models.Proyecto", "Proyecto")
                        .WithMany()
                        .HasForeignKey("ProyectoId");
                });

            modelBuilder.Entity("App.Models.Rúbrica", b =>
                {
                    b.HasOne("App.Models.Administrador", null)
                        .WithMany("Rúbricas")
                        .HasForeignKey("AdministradorId");
                });

            modelBuilder.Entity("App.Models.Sesión", b =>
                {
                    b.HasOne("App.Models.Usuario", "Usuario")
                        .WithMany("Sesiones")
                        .HasForeignKey("UsuarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
