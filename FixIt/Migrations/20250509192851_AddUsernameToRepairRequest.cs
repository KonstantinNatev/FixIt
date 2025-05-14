using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FixIt.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToRepairRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "RepairRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "RepairRequests");
        }
    }
}
