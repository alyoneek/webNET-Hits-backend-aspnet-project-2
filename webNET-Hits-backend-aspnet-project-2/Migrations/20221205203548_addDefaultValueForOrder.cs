using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNETHitsbackendaspnetproject2.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultValueForOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 5, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(5919),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 12, 6, 3, 30, 59, 277, DateTimeKind.Local).AddTicks(2600));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 6, 3, 30, 59, 277, DateTimeKind.Local).AddTicks(2600),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 12, 5, 20, 35, 48, 35, DateTimeKind.Utc).AddTicks(5919));
        }
    }
}
