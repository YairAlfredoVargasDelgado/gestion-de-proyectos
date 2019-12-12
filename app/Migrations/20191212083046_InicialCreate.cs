using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace app.Migrations
{
    public partial class InicialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identificación = table.Column<string>(nullable: true),
                    Sexo = table.Column<int>(nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    PrimerApellido = table.Column<string>(nullable: true),
                    SegundoApellido = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identificación = table.Column<string>(nullable: true),
                    Sexo = table.Column<int>(nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    PrimerApellido = table.Column<string>(nullable: true),
                    SegundoApellido = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 16, nullable: false),
                    Contraseña = table.Column<string>(maxLength: 16, nullable: false),
                    CorreoElectrónico = table.Column<string>(nullable: true),
                    Rol = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rúbrica",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaDeRegistro = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 45, nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    CalificaciónMáxima = table.Column<int>(nullable: false),
                    AdministradorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rúbrica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rúbrica_Administrador_AdministradorId",
                        column: x => x.AdministradorId,
                        principalTable: "Administrador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asignatura",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Código = table.Column<string>(maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    CalificadorId = table.Column<long>(nullable: true),
                    DirectorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asignatura_Usuario_CalificadorId",
                        column: x => x.CalificadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asignatura_Usuario_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sesión",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<long>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesión", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sesión_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Criterio",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripción = table.Column<string>(nullable: true),
                    Porcentaje = table.Column<int>(nullable: false),
                    RúbricaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterio_Rúbrica_RúbricaId",
                        column: x => x.RúbricaId,
                        principalTable: "Rúbrica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Código = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Descripción = table.Column<string>(nullable: false),
                    AsignaturaId = table.Column<long>(nullable: true),
                    DirectorId = table.Column<long>(nullable: true),
                    Calificador1Id = table.Column<long>(nullable: true),
                    Calificador2Id = table.Column<long>(nullable: true),
                    RúbricaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyecto_Asignatura_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proyecto_Usuario_Calificador1Id",
                        column: x => x.Calificador1Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proyecto_Usuario_Calificador2Id",
                        column: x => x.Calificador2Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proyecto_Usuario_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proyecto_Rúbrica_RúbricaId",
                        column: x => x.RúbricaId,
                        principalTable: "Rúbrica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstudianteProyecto",
                columns: table => new
                {
                    IdEstudiante = table.Column<long>(nullable: false),
                    IdProyecto = table.Column<long>(nullable: false),
                    EstudianteId = table.Column<long>(nullable: true),
                    ProyectoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudianteProyecto", x => new { x.IdEstudiante, x.IdProyecto });
                    table.ForeignKey(
                        name: "FK_EstudianteProyecto_Estudiante_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstudianteProyecto_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoCalificador",
                columns: table => new
                {
                    IdProyecto = table.Column<long>(nullable: false),
                    IdCalificador = table.Column<long>(nullable: false),
                    ProyectoId = table.Column<long>(nullable: true),
                    CalificadorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoCalificador", x => new { x.IdCalificador, x.IdProyecto });
                    table.ForeignKey(
                        name: "FK_ProyectoCalificador_Usuario_CalificadorId",
                        column: x => x.CalificadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProyectoCalificador_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignatura_CalificadorId",
                table: "Asignatura",
                column: "CalificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Asignatura_DirectorId",
                table: "Asignatura",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterio_RúbricaId",
                table: "Criterio",
                column: "RúbricaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteProyecto_EstudianteId",
                table: "EstudianteProyecto",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteProyecto_ProyectoId",
                table: "EstudianteProyecto",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_AsignaturaId",
                table: "Proyecto",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_Calificador1Id",
                table: "Proyecto",
                column: "Calificador1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_Calificador2Id",
                table: "Proyecto",
                column: "Calificador2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_DirectorId",
                table: "Proyecto",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_RúbricaId",
                table: "Proyecto",
                column: "RúbricaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoCalificador_CalificadorId",
                table: "ProyectoCalificador",
                column: "CalificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoCalificador_ProyectoId",
                table: "ProyectoCalificador",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rúbrica_AdministradorId",
                table: "Rúbrica",
                column: "AdministradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sesión_UsuarioId",
                table: "Sesión",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Criterio");

            migrationBuilder.DropTable(
                name: "EstudianteProyecto");

            migrationBuilder.DropTable(
                name: "ProyectoCalificador");

            migrationBuilder.DropTable(
                name: "Sesión");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropTable(
                name: "Asignatura");

            migrationBuilder.DropTable(
                name: "Rúbrica");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Administrador");
        }
    }
}
