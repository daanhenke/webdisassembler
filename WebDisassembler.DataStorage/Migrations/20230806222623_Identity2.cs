using System;
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
            migrationBuilder.DeleteData(
                table: "TenantUser",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("aaa53b09-58d2-45e4-bca4-a7f4c7cb3f4f"), new Guid("c1bf6ab4-fd41-48fa-80e4-eb5b48172009") });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumns: new[] { "Id", "TenantId" },
                keyValues: new object[] { new Guid("6855af0a-e544-4125-b3cb-cc43b662dfde"), new Guid("aaa53b09-58d2-45e4-bca4-a7f4c7cb3f4f") });

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("aaa53b09-58d2-45e4-bca4-a7f4c7cb3f4f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1bf6ab4-fd41-48fa-80e4-eb5b48172009"));

            migrationBuilder.RenameColumn(
                name: "GeneratedAt",
                table: "AuthenticationToken",
                newName: "ExpiresBy");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "ExpiresBy",
                table: "AuthenticationToken",
                newName: "GeneratedAt");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { new Guid("c1bf6ab4-fd41-48fa-80e4-eb5b48172009"), "admin@webdisassembler.io", "123", "admin" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "Subdomain", "UserId" },
                values: new object[] { new Guid("aaa53b09-58d2-45e4-bca4-a7f4c7cb3f4f"), "Admin Tenant", "admin", new Guid("c1bf6ab4-fd41-48fa-80e4-eb5b48172009") });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "TenantId", "IsAdministrator", "Name" },
                values: new object[] { new Guid("6855af0a-e544-4125-b3cb-cc43b662dfde"), new Guid("aaa53b09-58d2-45e4-bca4-a7f4c7cb3f4f"), true, "Admin Role" });

            migrationBuilder.InsertData(
                table: "TenantUser",
                columns: new[] { "TenantId", "UserId", "RoleId" },
                values: new object[] { new Guid("aaa53b09-58d2-45e4-bca4-a7f4c7cb3f4f"), new Guid("c1bf6ab4-fd41-48fa-80e4-eb5b48172009"), new Guid("6855af0a-e544-4125-b3cb-cc43b662dfde") });
        }
    }
}
