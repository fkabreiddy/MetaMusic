using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class MejoraReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Albumes_AlbumId",
                table: "Calificaciones");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AlbumId",
                table: "Calificaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Albumes_AlbumId",
                table: "Calificaciones",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Albumes_AlbumId",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "AlbumId",
                table: "Calificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Albumes_AlbumId",
                table: "Calificaciones",
                column: "AlbumId",
                principalTable: "Albumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
