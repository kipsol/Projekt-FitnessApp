using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class MoveDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "ClassSchedules");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "ClassEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "ClassEvents");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "ClassSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
