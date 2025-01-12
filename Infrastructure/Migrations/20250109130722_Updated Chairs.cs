using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedChairs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RestaurantId",
                table: "Chairs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_RestaurantId",
                table: "Chairs",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Restaurants_RestaurantId",
                table: "Chairs",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Restaurants_RestaurantId",
                table: "Chairs");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_RestaurantId",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Chairs");
        }
    }
}
