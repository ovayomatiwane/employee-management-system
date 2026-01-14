using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string configScripts = @"..\WebApi\Data\ConfigScripts\Seed Roles Data.sql";

            string script = File.ReadAllText(configScripts);
            migrationBuilder.Sql($"EXEC(N'{script}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new NotSupportedException();
        }
    }
}
