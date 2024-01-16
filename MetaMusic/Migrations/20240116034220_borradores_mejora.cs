using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class borradores_mejora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
