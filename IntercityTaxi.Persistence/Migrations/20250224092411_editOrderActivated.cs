using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntercityTaxi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editOrderActivated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "Orders");
        }
    }
}
