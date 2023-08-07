using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class Identity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Tenants_TenantId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Users_UserId",
                table: "Tenants");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUser_Role_RoleId_TenantId",
                table: "TenantUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUser_Tenants_TenantId",
                table: "TenantUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUser_Users_UserId",
                table: "TenantUser");

            migrationBuilder.DropIndex(
                name: "IX_TenantUser_RoleId_TenantId",
                table: "TenantUser");

            migrationBuilder.DropIndex(
                name: "IX_TenantUser_TenantId",
                table: "TenantUser");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_UserId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Role_TenantId",
                table: "Role");

            migrationBuilder.DeleteData(
                table: "TenantUser",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("f074b57c-7c5f-4c0f-9af7-fa12065bc74e"), new Guid("aab5ca3e-5859-4f6c-a33b-b806945b4adf") });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumns: new[] { "Id", "TenantId" },
                keyValues: new object[] { new Guid("da8e7e13-6573-454b-b4db-dd9183f8210c"), new Guid("f074b57c-7c5f-4c0f-9af7-fa12065bc74e") });

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("f074b57c-7c5f-4c0f-9af7-fa12065bc74e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aab5ca3e-5859-4f6c-a33b-b806945b4adf"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "TenantId", "IsAdministrator", "Name" },
                values: new object[] { new Guid("d59092cd-b731-4b99-90a7-9218ab1cb712"), new Guid("05866dd7-56ef-4152-8b37-c0eff10202fb"), true, "Admin Role" });

            migrationBuilder.InsertData(
                table: "TenantUser",
                columns: new[] { "TenantId", "UserId", "RoleId" },
                values: new object[] { new Guid("05866dd7-56ef-4152-8b37-c0eff10202fb"), new Guid("552aadad-6abc-43a0-92f8-f35c751a7edf"), new Guid("d59092cd-b731-4b99-90a7-9218ab1cb712") });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "Subdomain", "UserId" },
                values: new object[] { new Guid("05866dd7-56ef-4152-8b37-c0eff10202fb"), "Admin Tenant", "admin", new Guid("552aadad-6abc-43a0-92f8-f35c751a7edf") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { new Guid("552aadad-6abc-43a0-92f8-f35c751a7edf"), "admin@webdisassembler.io", "123", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumns: new[] { "Id", "TenantId" },
                keyValues: new object[] { new Guid("d59092cd-b731-4b99-90a7-9218ab1cb712"), new Guid("05866dd7-56ef-4152-8b37-c0eff10202fb") });

            migrationBuilder.DeleteData(
                table: "TenantUser",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("05866dd7-56ef-4152-8b37-c0eff10202fb"), new Guid("552aadad-6abc-43a0-92f8-f35c751a7edf") });

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("05866dd7-56ef-4152-8b37-c0eff10202fb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("552aadad-6abc-43a0-92f8-f35c751a7edf"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { new Guid("aab5ca3e-5859-4f6c-a33b-b806945b4adf"), "admin@webdisassembler.io", "123", "admin" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "Subdomain", "UserId" },
                values: new object[] { new Guid("f074b57c-7c5f-4c0f-9af7-fa12065bc74e"), "Admin Tenant", "admin", new Guid("aab5ca3e-5859-4f6c-a33b-b806945b4adf") });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "TenantId", "IsAdministrator", "Name" },
                values: new object[] { new Guid("da8e7e13-6573-454b-b4db-dd9183f8210c"), new Guid("f074b57c-7c5f-4c0f-9af7-fa12065bc74e"), true, "Admin Role" });

            migrationBuilder.InsertData(
                table: "TenantUser",
                columns: new[] { "TenantId", "UserId", "RoleId" },
                values: new object[] { new Guid("f074b57c-7c5f-4c0f-9af7-fa12065bc74e"), new Guid("aab5ca3e-5859-4f6c-a33b-b806945b4adf"), new Guid("da8e7e13-6573-454b-b4db-dd9183f8210c") });

            migrationBuilder.CreateIndex(
                name: "IX_TenantUser_RoleId_TenantId",
                table: "TenantUser",
                columns: new[] { "RoleId", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_TenantUser_TenantId",
                table: "TenantUser",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_UserId",
                table: "Tenants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_TenantId",
                table: "Role",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Tenants_TenantId",
                table: "Role",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Users_UserId",
                table: "Tenants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUser_Role_RoleId_TenantId",
                table: "TenantUser",
                columns: new[] { "RoleId", "TenantId" },
                principalTable: "Role",
                principalColumns: new[] { "Id", "TenantId" },
                onDelete: ReferentialAction.Cascade);

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
    }
}
