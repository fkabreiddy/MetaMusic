using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class tablaBorradores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artista_Albumes_AlbumId",
                table: "Album_Artista");

            migrationBuilder.DropForeignKey(
                name: "FK_Albumes_Usuarios_CreadorId",
                table: "Albumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Albumes_AlbumId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Albumes_AlbumId",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificacions_Albumes_AlbumId",
                table: "Notificacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Peticiones_Albumes_AlbumId",
                table: "Peticiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Albumes_AlbumId",
                table: "Tracks");

            migrationBuilder.DropTable(
                name: "Borradores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albumes",
                table: "Albumes");

            migrationBuilder.RenameTable(
                name: "Albumes",
                newName: "Album");

            migrationBuilder.RenameIndex(
                name: "IX_Albumes_CreadorId",
                table: "Album",
                newName: "IX_Album_CreadorId");

            migrationBuilder.AddColumn<bool>(
                name: "Publicado",
                table: "Album",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "YaExiste",
                table: "Album",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Usuarios_CreadorId",
                table: "Album",
                column: "CreadorId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artista_Album_AlbumId",
                table: "Album_Artista",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Album_AlbumId",
                table: "Calificaciones",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Album_AlbumId",
                table: "Notas",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacions_Album_AlbumId",
                table: "Notificacions",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Peticiones_Album_AlbumId",
                table: "Peticiones",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Album_IdAlbum",
                table: "Reviews",
                column: "IdAlbum",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Album_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Usuarios_CreadorId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artista_Album_AlbumId",
                table: "Album_Artista");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Album_AlbumId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Album_AlbumId",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificacions_Album_AlbumId",
                table: "Notificacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Peticiones_Album_AlbumId",
                table: "Peticiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Album_IdAlbum",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Album_AlbumId",
                table: "Tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "Publicado",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "YaExiste",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Albumes");

            migrationBuilder.RenameIndex(
                name: "IX_Album_CreadorId",
                table: "Albumes",
                newName: "IX_Albumes_CreadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albumes",
                table: "Albumes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Borradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borradores_Albumes_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borradores_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borradores_AlbumId",
                table: "Borradores",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Borradores_UsuarioId",
                table: "Borradores",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artista_Albumes_AlbumId",
                table: "Album_Artista",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albumes_Usuarios_CreadorId",
                table: "Albumes",
                column: "CreadorId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Albumes_AlbumId",
                table: "Calificaciones",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Albumes_AlbumId",
                table: "Notas",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacions_Albumes_AlbumId",
                table: "Notificacions",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Peticiones_Albumes_AlbumId",
                table: "Peticiones",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews",
                column: "IdAlbum",
                principalTable: "Albumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Albumes_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
