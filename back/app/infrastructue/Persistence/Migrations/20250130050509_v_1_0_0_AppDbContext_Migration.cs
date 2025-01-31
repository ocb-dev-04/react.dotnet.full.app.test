using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v_1_0_0_AppDbContext_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "permissions");

            migrationBuilder.CreateTable(
                name: "PermissionType",
                schema: "permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionDateOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EmployeeLastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false),
                    version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_PermissionType_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalSchema: "permissions",
                        principalTable: "PermissionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permission_EmployeeName_EmployeeLastName_PermissionTypeId",
                schema: "permissions",
                table: "Permission",
                columns: new[] { "EmployeeName", "EmployeeLastName", "PermissionTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionTypeId",
                schema: "permissions",
                table: "Permission",
                column: "PermissionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permission",
                schema: "permissions");

            migrationBuilder.DropTable(
                name: "PermissionType",
                schema: "permissions");
        }
    }
}
