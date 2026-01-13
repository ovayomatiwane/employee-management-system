using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class seedrolesdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantTasks_RoleRate_RoleRateId",
                table: "ConsultantTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleRate_Roles_RoleId",
                table: "RoleRate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleRate",
                table: "RoleRate");

            migrationBuilder.RenameTable(
                name: "RoleRate",
                newName: "RoleRates");

            migrationBuilder.RenameIndex(
                name: "IX_RoleRate_RoleId",
                table: "RoleRates",
                newName: "IX_RoleRates_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleRates",
                table: "RoleRates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantTasks_RoleRates_RoleRateId",
                table: "ConsultantTasks",
                column: "RoleRateId",
                principalTable: "RoleRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleRates_Roles_RoleId",
                table: "RoleRates",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantTasks_RoleRates_RoleRateId",
                table: "ConsultantTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleRates_Roles_RoleId",
                table: "RoleRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleRates",
                table: "RoleRates");

            migrationBuilder.RenameTable(
                name: "RoleRates",
                newName: "RoleRate");

            migrationBuilder.RenameIndex(
                name: "IX_RoleRates_RoleId",
                table: "RoleRate",
                newName: "IX_RoleRate_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleRate",
                table: "RoleRate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantTasks_RoleRate_RoleRateId",
                table: "ConsultantTasks",
                column: "RoleRateId",
                principalTable: "RoleRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleRate_Roles_RoleId",
                table: "RoleRate",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
