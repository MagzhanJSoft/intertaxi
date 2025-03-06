using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntercityTaxi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addRationg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Clients",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Clients");
        }
    }
}
