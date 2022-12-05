using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNETHitsbackendaspnetproject2.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultValueForOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "Delivered",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "InProcess");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 5, 20, 47, 28, 756, DateTimeKind.Utc).AddTicks(1335),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 12, 5, 20, 46, 4, 874, DateTimeKind.Utc).AddTicks(1233));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "InProcess",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Delivered");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 5, 20, 46, 4, 874, DateTimeKind.Utc).AddTicks(1233),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 12, 5, 20, 47, 28, 756, DateTimeKind.Utc).AddTicks(1335));
        }
    }
}
