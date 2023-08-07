using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class Identity6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("6ad2c1f1-c06b-43c8-986b-cbaab6970c56"), new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f"), true, "Admin Role" });

            migrationBuilder.InsertData(
                table: "TenantUser",
                columns: new[] { "TenantId", "UserId", "RoleId" },
                values: new object[] { new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f"), new Guid("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4"), new Guid("6ad2c1f1-c06b-43c8-986b-cbaab6970c56") });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "Subdomain", "UserId" },
                values: new object[] { new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f"), "Admin Tenant", "admin", new Guid("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { new Guid("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4"), "admin@webdisassembler.io", "123", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumns: new[] { "Id", "TenantId" },
                keyValues: new object[] { new Guid("6ad2c1f1-c06b-43c8-986b-cbaab6970c56"), new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f") });

            migrationBuilder.DeleteData(
                table: "TenantUser",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f"), new Guid("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4") });

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4"));

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
    }
}
