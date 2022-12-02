using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNETHitsbackendaspnetproject2.Migrations
{
    /// <inheritdoc />
    public partial class DishForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DishCAtegoryId",
                table: "Dishes",
                newName: "DishCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DishCategoryId",
                table: "Dishes",
                newName: "DishCAtegoryId");
        }
    }
}
