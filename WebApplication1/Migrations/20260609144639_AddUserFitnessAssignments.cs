using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFitnessAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFitnessAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanTreningowyId = table.Column<int>(type: "int", nullable: true),
                    DietId = table.Column<int>(type: "int", nullable: true),
                    AssignedByTrainerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFitnessAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFitnessAssignments_AspNetUsers_AssignedByTrainerId",
                        column: x => x.AssignedByTrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFitnessAssignments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFitnessAssignments_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserFitnessAssignments_PlanyTreningowe_PlanTreningowyId",
                        column: x => x.PlanTreningowyId,
                        principalTable: "PlanyTreningowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFitnessAssignments_AssignedByTrainerId",
                table: "UserFitnessAssignments",
                column: "AssignedByTrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFitnessAssignments_DietId",
                table: "UserFitnessAssignments",
                column: "DietId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFitnessAssignments_PlanTreningowyId",
                table: "UserFitnessAssignments",
                column: "PlanTreningowyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFitnessAssignments_UserId",
                table: "UserFitnessAssignments",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFitnessAssignments");
        }
    }
}
