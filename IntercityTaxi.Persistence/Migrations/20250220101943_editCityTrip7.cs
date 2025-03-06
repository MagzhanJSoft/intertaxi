using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntercityTaxi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editCityTrip7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_TripTypes_Trip",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Trip",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Trip",
                table: "Cities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Trip",
                table: "Cities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
