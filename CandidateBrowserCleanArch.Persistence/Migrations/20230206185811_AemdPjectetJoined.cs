using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateBrowserCleanArch.Persistence.Migrations
{
    public partial class AemdPjectetJoined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Projects_ProjectId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_ProjectId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Candidates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ProjectId",
                table: "Candidates",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Projects_ProjectId",
                table: "Candidates",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
