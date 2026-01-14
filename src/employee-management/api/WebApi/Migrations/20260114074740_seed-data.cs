using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        private const string CONFIG_SCRIPTS_FOLDER = @"..\WebApi\Data\ConfigScripts\";
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string configScriptName;


            //Read configs scripts into database
            configScriptName = "Seed Roles Data.sql";
            string script = File.ReadAllText($"{CONFIG_SCRIPTS_FOLDER}{configScriptName}");
            migrationBuilder.Sql($"EXEC(N'{script}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new NotSupportedException();
        }
    }
}
