using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Added_Business_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Requests",
                newName: "BusinessId");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.BusinessId);
                });

            migrationBuilder.CreateTable(
                name: "BusinessZakaziUser",
                columns: table => new
                {
                    BusinessesBusinessId = table.Column<int>(type: "int", nullable: false),
                    EmployeesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessZakaziUser", x => new { x.BusinessesBusinessId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_BusinessZakaziUser_AspNetUsers_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessZakaziUser_Businesses_BusinessesBusinessId",
                        column: x => x.BusinessesBusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BusinessId",
                table: "Requests",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ClientId",
                table: "Requests",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BusinessId",
                table: "Posts",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RequestId",
                table: "Posts",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessZakaziUser_EmployeesId",
                table: "BusinessZakaziUser",
                column: "EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Businesses_BusinessId",
                table: "Posts",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "BusinessId",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Businesses_BusinessId",
                table: "Requests",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "BusinessId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Businesses_BusinessId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Requests_RequestId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ClientId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Businesses_BusinessId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "BusinessZakaziUser");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Requests_BusinessId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ClientId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BusinessId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_RequestId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Requests",
                newName: "PostId");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "WorkerId",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkerId",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
