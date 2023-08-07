using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class BinaryExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "Binary",
                newName: "ProjectPath");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.AddColumn<decimal>(
                name: "Begin",
                table: "BinarySection",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "CanExecute",
                table: "BinarySection",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanRead",
                table: "BinarySection",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanWrite",
                table: "BinarySection",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "End",
                table: "BinarySection",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "AnalysisStatus",
                table: "Binary",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Dictionary<string, string>>(
                name: "Metadata",
                table: "Binary",
                type: "hstore",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Begin",
                table: "BinarySection");

            migrationBuilder.DropColumn(
                name: "CanExecute",
                table: "BinarySection");

            migrationBuilder.DropColumn(
                name: "CanRead",
                table: "BinarySection");

            migrationBuilder.DropColumn(
                name: "CanWrite",
                table: "BinarySection");

            migrationBuilder.DropColumn(
                name: "End",
                table: "BinarySection");

            migrationBuilder.DropColumn(
                name: "AnalysisStatus",
                table: "Binary");

            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "Binary");

            migrationBuilder.RenameColumn(
                name: "ProjectPath",
                table: "Binary",
                newName: "Filename");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");
        }
    }
}
