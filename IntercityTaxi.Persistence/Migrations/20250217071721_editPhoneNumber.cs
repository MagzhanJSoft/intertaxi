using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntercityTaxi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Clients",
                type: "real",
                nullable: false,
                defaultValue: 3f,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Clients",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldDefaultValue: 3f);
        }
    }
}
