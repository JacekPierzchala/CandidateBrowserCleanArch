using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateBrowserCleanArch.Persistence.Migrations
{
    public partial class PositionField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "CandidateCompanies",
                type: "nvarchar(250)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "CandidateCompanies");
        }
    }
}
