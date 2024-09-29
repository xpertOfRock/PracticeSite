using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeSite.Migrations
{
    /// <inheritdoc />
    public partial class Rework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_AspNetUsers_ApplicationUserId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_ApplicationUserId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Vacancies");

            migrationBuilder.CreateTable(
                name: "ApplicationUserVacancy",
                columns: table => new
                {
                    AppliedVacanciesId = table.Column<int>(type: "int", nullable: false),
                    RespondedUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserVacancy", x => new { x.AppliedVacanciesId, x.RespondedUsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserVacancy_AspNetUsers_RespondedUsersId",
                        column: x => x.RespondedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserVacancy_Vacancies_AppliedVacanciesId",
                        column: x => x.AppliedVacanciesId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserVacancy_RespondedUsersId",
                table: "ApplicationUserVacancy",
                column: "RespondedUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserVacancy");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Vacancies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ApplicationUserId",
                table: "Vacancies",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_AspNetUsers_ApplicationUserId",
                table: "Vacancies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
