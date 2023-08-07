using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class FileReferenceRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileReferences_Tenants_TenantId",
                table: "FileReferences");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FileReferences");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FileReferences");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FileReferences");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "FileReferences");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "FileReferences",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_FileReferences_Tenants_TenantId",
                table: "FileReferences",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileReferences_Tenants_TenantId",
                table: "FileReferences");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "FileReferences",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedAt",
                table: "FileReferences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "FileReferences",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "FileReferences",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "FileReferences",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileReferences_Tenants_TenantId",
                table: "FileReferences",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
