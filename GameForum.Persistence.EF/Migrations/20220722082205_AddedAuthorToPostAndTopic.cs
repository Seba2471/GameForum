using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForum.Persistence.EF.Migrations
{
    public partial class AddedAuthorToPostAndTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Topics",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_AuthorId",
                table: "Topics",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_AuthorId",
                table: "Topics",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_AuthorId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_AuthorId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Posts");
        }
    }
}
