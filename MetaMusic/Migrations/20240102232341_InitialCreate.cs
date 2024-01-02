using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoDePerfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Albumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSpotify = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Portada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albumes_Usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Artistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Foto_Perfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpotifyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artistas_Usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Busquedas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Busquedas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Busquedas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suscripciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuscriptorId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suscripciones_Usuarios_SuscriptorId",
                        column: x => x.SuscriptorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suscripciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Albumes_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadorId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Albumes_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albumes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notas_Usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notificacions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserToId = table.Column<int>(type: "int", nullable: false),
                    UserFromId = table.Column<int>(type: "int", nullable: true),
                    AlbumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacions_Albumes_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albumes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificacions_Usuarios_UserFromId",
                        column: x => x.UserFromId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificacions_Usuarios_UserToId",
                        column: x => x.UserToId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreadorId = table.Column<int>(type: "int", nullable: true),
                    IdAlbum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Albumes_IdAlbum",
                        column: x => x.IdAlbum,
                        principalTable: "Albumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Albumes_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Album_Artista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistaId = table.Column<int>(type: "int", nullable: true),
                    AlbumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album_Artista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Album_Artista_Albumes_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albumes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Album_Artista_Artistas_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "Artistas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genero_Artistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneroId = table.Column<int>(type: "int", nullable: true),
                    ArtistaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero_Artistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genero_Artistas_Artistas_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "Artistas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Genero_Artistas_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Dislike_Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    NotaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Dislike_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Dislike_Notas_Notas_NotaId",
                        column: x => x.NotaId,
                        principalTable: "Notas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Dislike_Notas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Like_Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    NotaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Like_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Like_Notas_Notas_NotaId",
                        column: x => x.NotaId,
                        principalTable: "Notas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Like_Notas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: true),
                    NotaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reportes_Notas_NotaId",
                        column: x => x.NotaId,
                        principalTable: "Notas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reportes_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Like_Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    TrackId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Like_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Like_Tracks_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Like_Tracks_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_Artista_AlbumId",
                table: "Album_Artista",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Album_Artista_ArtistaId",
                table: "Album_Artista",
                column: "ArtistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Albumes_CreadorId",
                table: "Albumes",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_CreadorId",
                table: "Artistas",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Busquedas_UsuarioId",
                table: "Busquedas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_AlbumId",
                table: "Calificaciones",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_UsuarioId",
                table: "Calificaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Genero_Artistas_ArtistaId",
                table: "Genero_Artistas",
                column: "ArtistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Genero_Artistas_GeneroId",
                table: "Genero_Artistas",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_AlbumId",
                table: "Notas",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_CreadorId",
                table: "Notas",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacions_AlbumId",
                table: "Notificacions",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacions_UserFromId",
                table: "Notificacions",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacions_UserToId",
                table: "Notificacions",
                column: "UserToId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_NotaId",
                table: "Reportes",
                column: "NotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_ReviewId",
                table: "Reportes",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_UsuarioId",
                table: "Reportes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreadorId",
                table: "Reviews",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IdAlbum",
                table: "Reviews",
                column: "IdAlbum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_SuscriptorId",
                table: "Suscripciones",
                column: "SuscriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_UsuarioId",
                table: "Suscripciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Dislike_Notas_NotaId",
                table: "Usuario_Dislike_Notas",
                column: "NotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Dislike_Notas_UsuarioId",
                table: "Usuario_Dislike_Notas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Like_Notas_NotaId",
                table: "Usuario_Like_Notas",
                column: "NotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Like_Notas_UsuarioId",
                table: "Usuario_Like_Notas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Like_Tracks_TrackId",
                table: "Usuario_Like_Tracks",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Like_Tracks_UsuarioId",
                table: "Usuario_Like_Tracks",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Album_Artista");

            migrationBuilder.DropTable(
                name: "Busquedas");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Genero_Artistas");

            migrationBuilder.DropTable(
                name: "Notificacions");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Suscripciones");

            migrationBuilder.DropTable(
                name: "Usuario_Dislike_Notas");

            migrationBuilder.DropTable(
                name: "Usuario_Like_Notas");

            migrationBuilder.DropTable(
                name: "Usuario_Like_Tracks");

            migrationBuilder.DropTable(
                name: "Artistas");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Albumes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
