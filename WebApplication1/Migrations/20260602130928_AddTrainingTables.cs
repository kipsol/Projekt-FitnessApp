using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseGymMachines");

            migrationBuilder.DropTable(
                name: "ExerciseMuscleGroups");

            migrationBuilder.DropTable(
                name: "TrainingPlanExercises");

            migrationBuilder.DropTable(
                name: "GymMachines");

            migrationBuilder.DropTable(
                name: "MuscleGroups");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "TrainingPlans");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.CreateTable(
                name: "PartieMiesniowe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartieMiesniowe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanyTreningowe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PoziomZaawansowania = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Cel = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CzasTrwaniaTygodnie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanyTreningowe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sekcje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Pietro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sekcje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cwiczenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    OpisWykonania = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PartiaMiesniowaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cwiczenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cwiczenia_PartieMiesniowe_PartiaMiesniowaId",
                        column: x => x.PartiaMiesniowaId,
                        principalTable: "PartieMiesniowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maszyny",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SekcjaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maszyny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maszyny_Sekcje_SekcjaId",
                        column: x => x.SekcjaId,
                        principalTable: "Sekcje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PozycjePlanu",
                columns: table => new
                {
                    PlanTreningowyId = table.Column<int>(type: "int", nullable: false),
                    CwiczenieId = table.Column<int>(type: "int", nullable: false),
                    DzienTreningowy = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LiczbaSerii = table.Column<int>(type: "int", nullable: false),
                    LiczbaPowtorzen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_PartiaMiesniowaId",
                table: "Cwiczenia",
                column: "PartiaMiesniowaId");

            migrationBuilder.CreateIndex(
                name: "IX_Maszyny_SekcjaId",
                table: "Maszyny",
                column: "SekcjaId");

            migrationBuilder.CreateIndex(
                name: "IX_MaszynyCwiczenia_CwiczenieId",
                table: "MaszynyCwiczenia",
                column: "CwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjePlanu_CwiczenieId",
                table: "PozycjePlanu",
                column: "CwiczenieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaszynyCwiczenia");

            migrationBuilder.DropTable(
                name: "PozycjePlanu");

            migrationBuilder.DropTable(
                name: "Maszyny");

            migrationBuilder.DropTable(
                name: "Cwiczenia");

            migrationBuilder.DropTable(
                name: "PlanyTreningowe");

            migrationBuilder.DropTable(
                name: "Sekcje");

            migrationBuilder.DropTable(
                name: "PartieMiesniowe");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    TechniqueTips = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DurationWeeks = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GymMachines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymMachines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymMachines_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MuscleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuscleGroups_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlanExercises",
                columns: table => new
                {
                    TrainingPlanId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    RestSeconds = table.Column<int>(type: "int", nullable: true),
                    Sets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlanExercises", x => new { x.TrainingPlanId, x.ExerciseId, x.DayNumber, x.Order });
                    table.ForeignKey(
                        name: "FK_TrainingPlanExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingPlanExercises_TrainingPlans_TrainingPlanId",
                        column: x => x.TrainingPlanId,
                        principalTable: "TrainingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseGymMachines",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    GymMachineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseGymMachines", x => new { x.ExerciseId, x.GymMachineId });
                    table.ForeignKey(
                        name: "FK_ExerciseGymMachines_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseGymMachines_GymMachines_GymMachineId",
                        column: x => x.GymMachineId,
                        principalTable: "GymMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseMuscleGroups",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    MuscleGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMuscleGroups", x => new { x.ExerciseId, x.MuscleGroupId });
                    table.ForeignKey(
                        name: "FK_ExerciseMuscleGroups_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseMuscleGroups_MuscleGroups_MuscleGroupId",
                        column: x => x.MuscleGroupId,
                        principalTable: "MuscleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseGymMachines_GymMachineId",
                table: "ExerciseGymMachines",
                column: "GymMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMuscleGroups_MuscleGroupId",
                table: "ExerciseMuscleGroups",
                column: "MuscleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GymMachines_SectionId",
                table: "GymMachines",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleGroups_SectionId",
                table: "MuscleGroups",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlanExercises_ExerciseId",
                table: "TrainingPlanExercises",
                column: "ExerciseId");
        }
    }
}

