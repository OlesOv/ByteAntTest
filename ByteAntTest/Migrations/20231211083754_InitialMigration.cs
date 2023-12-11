using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteAntTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    PositionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.PositionID);
                    table.ForeignKey(
                        name: "FK_Position_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_Position_Position_ReportsToID",
                        column: x => x.ReportsToID,
                        principalTable: "Position",
                        principalColumn: "PositionID");
                    table.ForeignKey(
                        name: "FK_Position_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Position_EmployeeID",
                table: "Position",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Position_ReportsToID",
                table: "Position",
                column: "ReportsToID");

            migrationBuilder.CreateIndex(
                name: "IX_Position_RoleID",
                table: "Position",
                column: "RoleID");

            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG ft AS DEFAULT;\r\nCREATE FULLTEXT INDEX ON Employee(Name)\r\n   KEY INDEX PK_Employee\r\n   WITH STOPLIST = SYSTEM;\r\nGO",
                suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
