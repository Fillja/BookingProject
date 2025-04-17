using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredEverythingRemovedChairSeating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Seatings_SeatingId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Seatings");

            migrationBuilder.DropTable(
                name: "Chairs");

            migrationBuilder.RenameColumn(
                name: "SeatingId",
                table: "Bookings",
                newName: "TableId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_SeatingId",
                table: "Bookings",
                newName: "IX_Bookings_TableId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Tables_TableId",
                table: "Bookings",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Tables_TableId",
                table: "Bookings");

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

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "Bookings",
                newName: "SeatingId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_TableId",
                table: "Bookings",
                newName: "IX_Bookings_SeatingId");

            migrationBuilder.CreateTable(
                name: "Chairs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestaurantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Eggs = table.Column<bool>(type: "bit", nullable: false),
                    Gluten = table.Column<bool>(type: "bit", nullable: false),
                    Milk = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vegan = table.Column<bool>(type: "bit", nullable: false),
                    Vegetarian = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chairs_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seatings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChairId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seatings_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seatings_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_RestaurantId",
                table: "Chairs",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_ChairId",
                table: "Seatings",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_TableId",
                table: "Seatings",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Seatings_SeatingId",
                table: "Bookings",
                column: "SeatingId",
                principalTable: "Seatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
