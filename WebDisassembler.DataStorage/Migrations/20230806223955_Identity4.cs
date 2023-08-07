using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class Identity4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Role",
                columns: new[] { "Id", "TenantId", "IsAdministrator", "Name" },
                values: new object[] { new Guid("badeff04-6a7c-4e07-b80a-65bc4f0910cc"), new Guid("c3c75874-1c8a-45ae-bc41-a0566bf2e802"), true, "Admin Role" });

            migrationBuilder.InsertData(
                table: "TenantUser",
                columns: new[] { "TenantId", "UserId", "RoleId" },
                values: new object[] { new Guid("c3c75874-1c8a-45ae-bc41-a0566bf2e802"), new Guid("0ce565b3-6994-45fd-8c92-97acee67d39a"), new Guid("badeff04-6a7c-4e07-b80a-65bc4f0910cc") });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "Subdomain", "UserId" },
                values: new object[] { new Guid("c3c75874-1c8a-45ae-bc41-a0566bf2e802"), "Admin Tenant", "admin", new Guid("0ce565b3-6994-45fd-8c92-97acee67d39a") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { new Guid("0ce565b3-6994-45fd-8c92-97acee67d39a"), "admin@webdisassembler.io", "123", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumns: new[] { "Id", "TenantId" },
                keyValues: new object[] { new Guid("badeff04-6a7c-4e07-b80a-65bc4f0910cc"), new Guid("c3c75874-1c8a-45ae-bc41-a0566bf2e802") });

            migrationBuilder.DeleteData(
                table: "TenantUser",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("c3c75874-1c8a-45ae-bc41-a0566bf2e802"), new Guid("0ce565b3-6994-45fd-8c92-97acee67d39a") });

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("c3c75874-1c8a-45ae-bc41-a0566bf2e802"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0ce565b3-6994-45fd-8c92-97acee67d39a"));

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
    }
}
