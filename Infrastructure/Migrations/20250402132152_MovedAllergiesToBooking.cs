using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MovedAllergiesToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eggs",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Gluten",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Milk",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Vegan",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Vegetarian",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "Eggs",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gluten",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lactose",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Milk",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vegan",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vegetarian",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eggs",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Gluten",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Lactose",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Milk",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Vegan",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Vegetarian",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "Eggs",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gluten",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Milk",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vegan",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vegetarian",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
