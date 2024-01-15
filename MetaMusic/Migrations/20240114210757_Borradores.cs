using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class Borradores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Notas_NotaId",
                table: "Reportes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Dislike_Notas_Notas_NotaId",
                table: "Usuario_Dislike_Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Like_Notas_Notas_NotaId",
                table: "Usuario_Like_Notas");

            migrationBuilder.CreateTable(
                name: "Borradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
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
                name: "FK_Reportes_Notas_NotaId",
                table: "Reportes",
                column: "NotaId",
                principalTable: "Notas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Dislike_Notas_Notas_NotaId",
                table: "Usuario_Dislike_Notas",
                column: "NotaId",
                principalTable: "Notas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Like_Notas_Notas_NotaId",
                table: "Usuario_Like_Notas",
                column: "NotaId",
                principalTable: "Notas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reportes_Notas_NotaId",
                table: "Reportes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Dislike_Notas_Notas_NotaId",
                table: "Usuario_Dislike_Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Like_Notas_Notas_NotaId",
                table: "Usuario_Like_Notas");

            migrationBuilder.DropTable(
                name: "Borradores");

            migrationBuilder.AddForeignKey(
                name: "FK_Reportes_Notas_NotaId",
                table: "Reportes",
                column: "NotaId",
                principalTable: "Notas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Dislike_Notas_Notas_NotaId",
                table: "Usuario_Dislike_Notas",
                column: "NotaId",
                principalTable: "Notas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Like_Notas_Notas_NotaId",
                table: "Usuario_Like_Notas",
                column: "NotaId",
                principalTable: "Notas",
                principalColumn: "Id");
        }
    }
}
