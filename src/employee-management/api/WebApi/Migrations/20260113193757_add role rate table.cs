using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class addroleratetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantTasks_Roles_RoleId",
                table: "ConsultantTasks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RemovedDate",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "ConsultantTasks",
                newName: "RoleRateId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsultantTasks_RoleId",
                table: "ConsultantTasks",
                newName: "IX_ConsultantTasks_RoleRateId");

            migrationBuilder.CreateTable(
                name: "RoleRate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HourlyRate = table.Column<float>(type: "real", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleRate_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleRate_RoleId",
                table: "RoleRate",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantTasks_RoleRate_RoleRateId",
                table: "ConsultantTasks",
                column: "RoleRateId",
                principalTable: "RoleRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantTasks_RoleRate_RoleRateId",
                table: "ConsultantTasks");

            migrationBuilder.DropTable(
                name: "RoleRate");

            migrationBuilder.RenameColumn(
                name: "RoleRateId",
                table: "ConsultantTasks",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsultantTasks_RoleRateId",
                table: "ConsultantTasks",
                newName: "IX_ConsultantTasks_RoleId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "HourlyRate",
                table: "Roles",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedDate",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantTasks_Roles_RoleId",
                table: "ConsultantTasks",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
