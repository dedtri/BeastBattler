using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelTime.Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class addedorigin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 21, 2, 23, 57, 980, DateTimeKind.Local).AddTicks(2011));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Origin",
                table: "trips");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 20, 18, 30, 12, 550, DateTimeKind.Local).AddTicks(3918));
        }
    }
}
