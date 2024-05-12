using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Changedrequestmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Requests_RequestId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ClientId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ClientId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Posts_RequestId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ClientId",
                table: "Requests",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RequestId",
                table: "Posts",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Requests_RequestId",
                table: "Posts",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ClientId",
                table: "Requests",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
