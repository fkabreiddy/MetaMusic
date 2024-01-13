using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class Mejora_Relaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artista_Albumes_AlbumId",
                table: "Album_Artista");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artista_Albumes_AlbumId",
                table: "Album_Artista",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews",
                column: "IdAlbum",
                principalTable: "Albumes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artista_Albumes_AlbumId",
                table: "Album_Artista");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artista_Albumes_AlbumId",
                table: "Album_Artista",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews",
                column: "IdAlbum",
                principalTable: "Albumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
