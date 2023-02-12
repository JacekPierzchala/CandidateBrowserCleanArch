using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateBrowserCleanArch.Persistence.Migrations
{
    public partial class AddPjectetJoined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateProject");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "CandidateCompanies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CandidateProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CandidateProjectId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateProjects_CandidateProjects_CandidateProjectId",
                        column: x => x.CandidateProjectId,
                        principalTable: "CandidateProjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateProjects_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ProjectId",
                table: "Candidates",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProjects_CandidateId",
                table: "CandidateProjects",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProjects_CandidateProjectId",
                table: "CandidateProjects",
                column: "CandidateProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProjects_ProjectId",
                table: "CandidateProjects",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Projects_ProjectId",
                table: "Candidates",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Projects_ProjectId",
                table: "Candidates");

            migrationBuilder.DropTable(
                name: "CandidateProjects");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_ProjectId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CandidateCompanies");

            migrationBuilder.CreateTable(
                name: "CandidateProject",
                columns: table => new
                {
                    CandidatesId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateProject", x => new { x.CandidatesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_CandidateProject_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProject_ProjectsId",
                table: "CandidateProject",
                column: "ProjectsId");
        }
    }
}
