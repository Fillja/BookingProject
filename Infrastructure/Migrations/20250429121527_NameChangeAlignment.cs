using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NameChangeAlignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "topAlignment",
                table: "Tables",
                newName: "TopAlignment");

            migrationBuilder.RenameColumn(
                name: "leftAlignment",
                table: "Tables",
                newName: "LeftAlignment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopAlignment",
                table: "Tables",
                newName: "topAlignment");

            migrationBuilder.RenameColumn(
                name: "LeftAlignment",
                table: "Tables",
                newName: "leftAlignment");
        }
    }
}
