using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WalletTrackerExpenseLimitAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LimitIsActive",
                table: "ExpenseCategoriesAssignedToUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitIsActive",
                table: "ExpenseCategoriesAssignedToUsers");
        }
    }
}
