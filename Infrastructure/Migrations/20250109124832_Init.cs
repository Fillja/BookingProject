using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chairs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Vegan = table.Column<bool>(type: "bit", nullable: false),
                    Vegetarian = table.Column<bool>(type: "bit", nullable: false),
                    Milk = table.Column<bool>(type: "bit", nullable: false),
                    Eggs = table.Column<bool>(type: "bit", nullable: false),
                    Gluten = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    RestaurantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TablesChairs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChairId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablesChairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablesChairs_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TablesChairs_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seatings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableChairId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestaurantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatingId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Seatings_SeatingId",
                        column: x => x.SeatingId,
                        principalTable: "Seatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SeatingId",
                table: "Bookings",
                column: "SeatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_RestaurantId",
                table: "Seatings",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_TableChairId",
                table: "Seatings",
                column: "TableChairId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_TablesChairs_ChairId",
                table: "TablesChairs",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_TablesChairs_TableId",
                table: "TablesChairs",
                column: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Seatings");

            migrationBuilder.DropTable(
                name: "TablesChairs");

            migrationBuilder.DropTable(
                name: "Chairs");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
