using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class Identity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TenantUser_TenantId",
                table: "TenantUser",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUser_Tenants_TenantId",
                table: "TenantUser",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUser_Users_UserId",
                table: "TenantUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantUser_Tenants_TenantId",
                table: "TenantUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUser_Users_UserId",
                table: "TenantUser");

            migrationBuilder.DropIndex(
                name: "IX_TenantUser_TenantId",
                table: "TenantUser");
        }
    }
}
