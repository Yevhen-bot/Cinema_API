using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Ch7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 0, 51, 9, 789, DateTimeKind.Local).AddTicks(9581),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 0, 49, 12, 85, DateTimeKind.Local).AddTicks(7366));

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 0, 49, 12, 85, DateTimeKind.Local).AddTicks(7366),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 0, 51, 9, 789, DateTimeKind.Local).AddTicks(9581));

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
