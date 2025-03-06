using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntercityTaxi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editCityTrip5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cities_Trip",
                table: "Cities",
                column: "Trip");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_TripTypes_Trip",
                table: "Cities",
                column: "Trip",
                principalTable: "TripTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_TripTypes_Trip",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Trip",
                table: "Cities");
        }
    }
}
