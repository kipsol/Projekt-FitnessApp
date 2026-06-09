using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddPozycjePlanuManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PozycjePlanu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanTreningowyId = table.Column<int>(type: "int", nullable: false),
                    CwiczenieId = table.Column<int>(type: "int", nullable: false),
                    DzienTreningowy = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LiczbaSerii = table.Column<int>(type: "int", nullable: false),
                    LiczbaPowtorzen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PrzerwaSekundy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjePlanu", x => x.Id);
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
                name: "IX_PozycjePlanu_CwiczenieId",
                table: "PozycjePlanu",
                column: "CwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjePlanu_PlanTreningowyId_CwiczenieId",
                table: "PozycjePlanu",
                columns: new[] { "PlanTreningowyId", "CwiczenieId" });

            migrationBuilder.Sql("""
                INSERT INTO PozycjePlanu (PlanTreningowyId, CwiczenieId, DzienTreningowy, LiczbaSerii, LiczbaPowtorzen, PrzerwaSekundy)
                SELECT PlanTreningowyId, Id, N'Do ustalenia', LiczbaSerii, LiczbaPowtorzen, PrzerwaSekundy
                FROM Cwiczenia
                WHERE PlanTreningowyId IS NOT NULL
                """);

            migrationBuilder.DropForeignKey(
                name: "FK_Cwiczenia_PlanyTreningowe_PlanTreningowyId",
                table: "Cwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_Cwiczenia_PlanTreningowyId",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "PlanTreningowyId",
                table: "Cwiczenia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PozycjePlanu");

            migrationBuilder.AddColumn<int>(
                name: "PlanTreningowyId",
                table: "Cwiczenia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_PlanTreningowyId",
                table: "Cwiczenia",
                column: "PlanTreningowyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cwiczenia_PlanyTreningowe_PlanTreningowyId",
                table: "Cwiczenia",
                column: "PlanTreningowyId",
                principalTable: "PlanyTreningowe",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
