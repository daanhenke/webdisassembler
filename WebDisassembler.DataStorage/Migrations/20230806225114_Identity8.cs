using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class Identity8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthenticationToken",
                table: "AuthenticationToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthenticationToken",
                table: "AuthenticationToken",
                columns: new[] { "Token", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationToken_Token",
                table: "AuthenticationToken",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthenticationToken",
                table: "AuthenticationToken");

            migrationBuilder.DropIndex(
                name: "IX_AuthenticationToken_Token",
                table: "AuthenticationToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthenticationToken",
                table: "AuthenticationToken",
                column: "Token");
        }
    }
}
