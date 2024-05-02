using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WalletTrackerExpenseNoCascadeToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_PaymentMethodsAssignedToUsers_PaymentId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethodsAssignedToUsers_AspNetUsers_UserId",
                table: "PaymentMethodsAssignedToUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_PaymentMethodsAssignedToUsers_PaymentId",
                table: "Expenses",
                column: "PaymentId",
                principalTable: "PaymentMethodsAssignedToUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethodsAssignedToUsers_AspNetUsers_UserId",
                table: "PaymentMethodsAssignedToUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_PaymentMethodsAssignedToUsers_PaymentId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethodsAssignedToUsers_AspNetUsers_UserId",
                table: "PaymentMethodsAssignedToUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_PaymentMethodsAssignedToUsers_PaymentId",
                table: "Expenses",
                column: "PaymentId",
                principalTable: "PaymentMethodsAssignedToUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethodsAssignedToUsers_AspNetUsers_UserId",
                table: "PaymentMethodsAssignedToUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
