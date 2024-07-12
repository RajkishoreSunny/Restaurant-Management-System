using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rms.Migrations
{
    /// <inheritdoc />
    public partial class Eighth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderHistory",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderHistory",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
