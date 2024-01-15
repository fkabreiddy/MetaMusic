using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class mejores_relaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Like_Tracks_Tracks_TrackId",
                table: "Usuario_Like_Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Like_Tracks_Usuarios_UsuarioId",
                table: "Usuario_Like_Tracks");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews",
                column: "IdAlbum",
                principalTable: "Albumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Like_Tracks_Tracks_TrackId",
                table: "Usuario_Like_Tracks",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Like_Tracks_Usuarios_UsuarioId",
                table: "Usuario_Like_Tracks",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Like_Tracks_Tracks_TrackId",
                table: "Usuario_Like_Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Like_Tracks_Usuarios_UsuarioId",
                table: "Usuario_Like_Tracks");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Albumes_IdAlbum",
                table: "Reviews",
                column: "IdAlbum",
                principalTable: "Albumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Like_Tracks_Tracks_TrackId",
                table: "Usuario_Like_Tracks",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Like_Tracks_Usuarios_UsuarioId",
                table: "Usuario_Like_Tracks",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
