using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ch6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 0, 49, 12, 85, DateTimeKind.Local).AddTicks(7366),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 30, 20, 22, 4, 659, DateTimeKind.Local).AddTicks(8095));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 30, 20, 22, 4, 659, DateTimeKind.Local).AddTicks(8095),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 0, 49, 12, 85, DateTimeKind.Local).AddTicks(7366));
        }
    }
}
