using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForum.Persistence.EF.Migrations
{
    public partial class RolesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "578c424f-e185-499d-be23-c5840a4e058b", "c99bf30a-512e-4292-ad16-bca4040ab237", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5df4e6e2-384b-47c7-8e78-3adccc4525e8", "03cdd183-51c5-4edd-b850-f672ede00bda", "Moderator", "MODERATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3bc9e48-62f1-44f0-9115-10ff88a5fdc5", "218f03dd-7dd6-4d03-9e64-6b99c30de266", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "578c424f-e185-499d-be23-c5840a4e058b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5df4e6e2-384b-47c7-8e78-3adccc4525e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3bc9e48-62f1-44f0-9115-10ff88a5fdc5");
        }
    }
}
