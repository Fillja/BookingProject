using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookingEntitysTableChairRenamedToSeating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Seatings_SeatingId",
                table: "Bookings",
                column: "SeatingId",
                principalTable: "Seatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Seatings_SeatingId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "SeatingId",
                table: "Bookings",
                newName: "TableChairId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_SeatingId",
                table: "Bookings",
                newName: "IX_Bookings_TableChairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Seatings_TableChairId",
                table: "Bookings",
                column: "TableChairId",
                principalTable: "Seatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
