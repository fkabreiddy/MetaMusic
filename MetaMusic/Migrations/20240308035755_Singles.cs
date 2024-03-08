using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetaMusic.Migrations
{
    /// <inheritdoc />
    public partial class Singles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

          

            migrationBuilder.AddColumn<bool>(
                name: "IsMustListen",
                table: "Albumes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSingle",
                table: "Albumes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Reference",
                table: "Albumes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMustListen",
                table: "Albumes");

            migrationBuilder.DropColumn(
                name: "IsSingle",
                table: "Albumes");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Albumes");

          

          
        }
    }
}
