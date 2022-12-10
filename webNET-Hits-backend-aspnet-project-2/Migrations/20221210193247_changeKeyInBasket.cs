using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNETHitsbackendaspnetproject2.Migrations
{
    /// <inheritdoc />
    public partial class changeKeyInBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DishesInBasket",
                table: "DishesInBasket");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 10, 19, 32, 47, 108, DateTimeKind.Utc).AddTicks(152),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 12, 5, 20, 49, 26, 524, DateTimeKind.Utc).AddTicks(1993));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DishesInBasket",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishesInBasket",
                table: "DishesInBasket",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DishesInBasket_CartId",
                table: "DishesInBasket",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DishesInBasket",
                table: "DishesInBasket");

            migrationBuilder.DropIndex(
                name: "IX_DishesInBasket_CartId",
                table: "DishesInBasket");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DishesInBasket");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 5, 20, 49, 26, 524, DateTimeKind.Utc).AddTicks(1993),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 12, 10, 19, 32, 47, 108, DateTimeKind.Utc).AddTicks(152));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishesInBasket",
                table: "DishesInBasket",
                columns: new[] { "CartId", "DishId" });
        }
    }
}
