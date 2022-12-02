using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNETHitsbackendaspnetproject2.Migrations
{
    /// <inheritdoc />
    public partial class DishForeignKeyChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Dishes_DishCategoryId",
                table: "Dishes",
                column: "DishCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_DishCategories_DishCategoryId",
                table: "Dishes",
                column: "DishCategoryId",
                principalTable: "DishCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_DishCategories_DishCategoryId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_DishCategoryId",
                table: "Dishes");
        }
    }
}
