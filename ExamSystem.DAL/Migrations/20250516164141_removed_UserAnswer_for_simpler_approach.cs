using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removed_UserAnswer_for_simpler_approach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbUserAnswers");

            migrationBuilder.AddColumn<int>(
                name: "UserChoice",
                table: "TbQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserChoice",
                table: "TbQuestions");

            migrationBuilder.CreateTable(
                name: "TbUserAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserExamId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    SelectedAnswer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbUserAnswers_TbQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TbQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbUserAnswers_TbUserExams_UserExamId",
                        column: x => x.UserExamId,
                        principalTable: "TbUserExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbUserAnswers_QuestionId",
                table: "TbUserAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUserAnswers_UserExamId",
                table: "TbUserAnswers",
                column: "UserExamId");
        }
    }
}
