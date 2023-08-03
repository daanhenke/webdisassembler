using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => new { x.OwnerId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BinaryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => new { x.OwnerId, x.ProjectId, x.BinaryId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Binaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectOwnerId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Binaries", x => new { x.OwnerId, x.ProjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_Binaries_Projects_ProjectOwnerId_ProjectId",
                        columns: x => new { x.ProjectOwnerId, x.ProjectId },
                        principalTable: "Projects",
                        principalColumns: new[] { "OwnerId", "Id" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Binaries_ProjectOwnerId_ProjectId",
                table: "Binaries",
                columns: new[] { "ProjectOwnerId", "ProjectId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Binaries");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
