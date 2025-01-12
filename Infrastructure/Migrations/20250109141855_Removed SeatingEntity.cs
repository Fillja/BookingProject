using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSeatingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Seatings_SeatingId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Seatings");

            migrationBuilder.RenameColumn(
                name: "SeatingId",
                table: "Bookings",
                newName: "TableChairId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_SeatingId",
                table: "Bookings",
                newName: "IX_Bookings_TableChairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TablesChairs_TableChairId",
                table: "Bookings",
                column: "TableChairId",
                principalTable: "TablesChairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TablesChairs_TableChairId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "TableChairId",
                table: "Bookings",
                newName: "SeatingId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_TableChairId",
                table: "Bookings",
                newName: "IX_Bookings_SeatingId");

            migrationBuilder.CreateTable(
                name: "Seatings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestaurantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableChairId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seatings_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seatings_TablesChairs_TableChairId",
                        column: x => x.TableChairId,
                        principalTable: "TablesChairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_RestaurantId",
                table: "Seatings",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_TableChairId",
                table: "Seatings",
                column: "TableChairId");

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
