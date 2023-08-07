using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class Identity5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("a2d059bd-28be-453d-bc6c-c706343d70ff"), new Guid("00539fe7-acb6-438b-be9f-6ca0b6a0c6bc"), true, "Admin Role" });

            migrationBuilder.InsertData(
                table: "TenantUser",
                columns: new[] { "TenantId", "UserId", "RoleId" },
                values: new object[] { new Guid("00539fe7-acb6-438b-be9f-6ca0b6a0c6bc"), new Guid("8e3118f9-adc5-4b13-9cd4-cd34549b4a48"), new Guid("a2d059bd-28be-453d-bc6c-c706343d70ff") });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "Subdomain", "UserId" },
                values: new object[] { new Guid("00539fe7-acb6-438b-be9f-6ca0b6a0c6bc"), "Admin Tenant", "admin", new Guid("8e3118f9-adc5-4b13-9cd4-cd34549b4a48") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { new Guid("8e3118f9-adc5-4b13-9cd4-cd34549b4a48"), "admin@webdisassembler.io", "123", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumns: new[] { "Id", "TenantId" },
                keyValues: new object[] { new Guid("a2d059bd-28be-453d-bc6c-c706343d70ff"), new Guid("00539fe7-acb6-438b-be9f-6ca0b6a0c6bc") });

            migrationBuilder.DeleteData(
                table: "TenantUser",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("00539fe7-acb6-438b-be9f-6ca0b6a0c6bc"), new Guid("8e3118f9-adc5-4b13-9cd4-cd34549b4a48") });

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("00539fe7-acb6-438b-be9f-6ca0b6a0c6bc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8e3118f9-adc5-4b13-9cd4-cd34549b4a48"));

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
    }
}
