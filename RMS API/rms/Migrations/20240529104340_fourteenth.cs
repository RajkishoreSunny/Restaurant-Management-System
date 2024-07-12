using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rms.Migrations
{
    /// <inheritdoc />
    public partial class fourteenth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerEmail",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerEmail",
                table: "Managers");
        }
    }
}
