using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddExercisesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaszynyCwiczenia");

            migrationBuilder.DropTable(
                name: "PozycjePlanu");

            migrationBuilder.DropColumn(
                name: "Cel",
                table: "PlanyTreningowe");

            migrationBuilder.AddColumn<string>(
                name: "LiczbaPowtorzen",
                table: "Cwiczenia",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LiczbaSerii",
                table: "Cwiczenia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaszynaId",
                table: "Cwiczenia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanTreningowyId",
                table: "Cwiczenia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrzerwaSekundy",
                table: "Cwiczenia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_MaszynaId",
                table: "Cwiczenia",
                column: "MaszynaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_PlanTreningowyId",
                table: "Cwiczenia",
                column: "PlanTreningowyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cwiczenia_Maszyny_MaszynaId",
                table: "Cwiczenia",
                column: "MaszynaId",
                principalTable: "Maszyny",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Cwiczenia_PlanyTreningowe_PlanTreningowyId",
                table: "Cwiczenia",
                column: "PlanTreningowyId",
                principalTable: "PlanyTreningowe",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cwiczenia_Maszyny_MaszynaId",
                table: "Cwiczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_Cwiczenia_PlanyTreningowe_PlanTreningowyId",
                table: "Cwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_Cwiczenia_MaszynaId",
                table: "Cwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_Cwiczenia_PlanTreningowyId",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "LiczbaPowtorzen",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "LiczbaSerii",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "MaszynaId",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "PlanTreningowyId",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "PrzerwaSekundy",
                table: "Cwiczenia");

            migrationBuilder.AddColumn<string>(
                name: "Cel",
                table: "PlanyTreningowe",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MaszynyCwiczenia",
                columns: table => new
                {
                    MaszynaId = table.Column<int>(type: "int", nullable: false),
                    CwiczenieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaszynyCwiczenia", x => new { x.MaszynaId, x.CwiczenieId });
                    table.ForeignKey(
                        name: "FK_MaszynyCwiczenia_Cwiczenia_CwiczenieId",
                        column: x => x.CwiczenieId,
                        principalTable: "Cwiczenia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaszynyCwiczenia_Maszyny_MaszynaId",
                        column: x => x.MaszynaId,
                        principalTable: "Maszyny",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PozycjePlanu",
                columns: table => new
                {
                    PlanTreningowyId = table.Column<int>(type: "int", nullable: false),
                    CwiczenieId = table.Column<int>(type: "int", nullable: false),
                    DzienTreningowy = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LiczbaPowtorzen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LiczbaSerii = table.Column<int>(type: "int", nullable: false),
                    PrzerwaSekundy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjePlanu", x => new { x.PlanTreningowyId, x.CwiczenieId, x.DzienTreningowy });
                    table.ForeignKey(
                        name: "FK_PozycjePlanu_Cwiczenia_CwiczenieId",
                        column: x => x.CwiczenieId,
                        principalTable: "Cwiczenia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PozycjePlanu_PlanyTreningowe_PlanTreningowyId",
                        column: x => x.PlanTreningowyId,
                        principalTable: "PlanyTreningowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaszynyCwiczenia_CwiczenieId",
                table: "MaszynyCwiczenia",
                column: "CwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjePlanu_CwiczenieId",
                table: "PozycjePlanu",
                column: "CwiczenieId");
        }
    }
}

