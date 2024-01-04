using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class Artists_Suscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artista_Suscriptores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artista_Suscriptores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artista_Suscriptores_Artistas_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "Artistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artista_Suscriptores_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artista_Suscriptores_ArtistaId",
                table: "Artista_Suscriptores",
                column: "ArtistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Artista_Suscriptores_UsuarioId",
                table: "Artista_Suscriptores",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artista_Suscriptores");
        }
    }
}
