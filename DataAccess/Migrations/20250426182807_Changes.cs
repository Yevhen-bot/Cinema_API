using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Films_FilmId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Halls_HallId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_StatusTickets_StatusId",
                table: "Tickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 26, 21, 28, 7, 6, DateTimeKind.Local).AddTicks(5145),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 16, 23, 10, 42, 506, DateTimeKind.Local).AddTicks(4447));

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Films_FilmId",
                table: "Sessions",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Halls_HallId",
                table: "Sessions",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_StatusTickets_StatusId",
                table: "Tickets",
                column: "StatusId",
                principalTable: "StatusTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Films_FilmId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Halls_HallId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_StatusTickets_StatusId",
                table: "Tickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 16, 23, 10, 42, 506, DateTimeKind.Local).AddTicks(4447),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 26, 21, 28, 7, 6, DateTimeKind.Local).AddTicks(5145));

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Films_FilmId",
                table: "Sessions",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Halls_HallId",
                table: "Sessions",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_StatusTickets_StatusId",
                table: "Tickets",
                column: "StatusId",
                principalTable: "StatusTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
