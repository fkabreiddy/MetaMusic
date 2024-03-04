using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class PeticionesMejora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acumulaciones",
                table: "Peticiones");

            migrationBuilder.AlterColumn<string>(
                name: "ArtistaFoto",
                table: "Peticiones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlbumPortada",
                table: "Peticiones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ArtistaFoto",
                table: "Peticiones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlbumPortada",
                table: "Peticiones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Acumulaciones",
                table: "Peticiones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
