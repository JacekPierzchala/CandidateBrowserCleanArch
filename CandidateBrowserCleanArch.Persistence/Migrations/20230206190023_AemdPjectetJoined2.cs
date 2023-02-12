using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateBrowserCleanArch.Persistence.Migrations
{
    public partial class AemdPjectetJoined2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateProjects_CandidateProjects_CandidateProjectId",
                table: "CandidateProjects");

            migrationBuilder.DropIndex(
                name: "IX_CandidateProjects_CandidateProjectId",
                table: "CandidateProjects");

            migrationBuilder.DropColumn(
                name: "CandidateProjectId",
                table: "CandidateProjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateProjectId",
                table: "CandidateProjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProjects_CandidateProjectId",
                table: "CandidateProjects",
                column: "CandidateProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateProjects_CandidateProjects_CandidateProjectId",
                table: "CandidateProjects",
                column: "CandidateProjectId",
                principalTable: "CandidateProjects",
                principalColumn: "Id");
        }
    }
}
