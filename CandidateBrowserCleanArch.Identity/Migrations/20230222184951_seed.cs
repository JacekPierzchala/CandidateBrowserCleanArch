using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateBrowserCleanArch.Identity.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b2acf7b0-f953-4e02-ae6b-6a177c6783f6", "f5098f19-0bd9-4f80-9fd3-8bcd0f838760", "User", "USER" },
                    { "def680c0-d1b9-48d3-a411-f3fd28375b4f", "94d25656-46b4-4dbf-9209-52c7d47385c5", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0ab2a932-6fc4-4487-bfc2-f0dbd11bfdfe", 0, "9df41dc5-91ce-465e-a64c-00a503d733a5", "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAEAACcQAAAAEHu+m0tWiQLlBrAoD7OOxfsTeUB8NS4dQFolQCiY89UZ9ub2O6duJtLtKDkthYT0nQ==", null, false, "a4515a97-b9bb-4433-9ee4-ac215beb34b7", false, "admin@localhost.com" },
                    { "676439f5-178b-4869-804d-0d88e8c3b70b", 0, "ffd55929-2922-498f-a472-997884327761", "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAECrJbrnM1EZ8CF1wO8jqKdt+FuihtDqjBavpjeW8vJgC+M8dtyfT6tf24pEu6qjs8A==", null, false, "c84fac47-2c7d-44c0-921e-f6e08e9131ee", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "def680c0-d1b9-48d3-a411-f3fd28375b4f", "0ab2a932-6fc4-4487-bfc2-f0dbd11bfdfe" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b2acf7b0-f953-4e02-ae6b-6a177c6783f6", "676439f5-178b-4869-804d-0d88e8c3b70b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "def680c0-d1b9-48d3-a411-f3fd28375b4f", "0ab2a932-6fc4-4487-bfc2-f0dbd11bfdfe" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b2acf7b0-f953-4e02-ae6b-6a177c6783f6", "676439f5-178b-4869-804d-0d88e8c3b70b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2acf7b0-f953-4e02-ae6b-6a177c6783f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "def680c0-d1b9-48d3-a411-f3fd28375b4f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0ab2a932-6fc4-4487-bfc2-f0dbd11bfdfe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "676439f5-178b-4869-804d-0d88e8c3b70b");
        }
    }
}
