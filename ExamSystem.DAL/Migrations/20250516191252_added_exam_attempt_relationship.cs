using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added_exam_attempt_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitDate",
                table: "TbUserExams");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "TbUserExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbUserExams_ExamId",
                table: "TbUserExams",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbUserExams_TbExams_ExamId",
                table: "TbUserExams",
                column: "ExamId",
                principalTable: "TbExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbUserExams_TbExams_ExamId",
                table: "TbUserExams");

            migrationBuilder.DropIndex(
                name: "IX_TbUserExams_ExamId",
                table: "TbUserExams");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "TbUserExams");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitDate",
                table: "TbUserExams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
