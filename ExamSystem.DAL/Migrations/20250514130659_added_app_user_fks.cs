using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added_app_user_fks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TbUserExams",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TbExams",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TbUserExams_UserId",
                table: "TbUserExams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TbExams_UserId",
                table: "TbExams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbExams_AspNetUsers_UserId",
                table: "TbExams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TbUserExams_AspNetUsers_UserId",
                table: "TbUserExams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbExams_AspNetUsers_UserId",
                table: "TbExams");

            migrationBuilder.DropForeignKey(
                name: "FK_TbUserExams_AspNetUsers_UserId",
                table: "TbUserExams");

            migrationBuilder.DropIndex(
                name: "IX_TbUserExams_UserId",
                table: "TbUserExams");

            migrationBuilder.DropIndex(
                name: "IX_TbExams_UserId",
                table: "TbExams");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TbUserExams");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TbExams");
        }
    }
}
