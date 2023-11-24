using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiLabs.Dal.Migrations
{
    /// <inheritdoc />
    public partial class FinalDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookingRecipe_RecipeCategory_RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe");

            migrationBuilder.DropColumn(
                name: "Confirmed",
                schema: "public",
                table: "CookingRecipe");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CookingRecipe_RecipeCategory_RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe",
                column: "RecipeCategoryId",
                principalSchema: "public",
                principalTable: "RecipeCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookingRecipe_RecipeCategory_RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                schema: "public",
                table: "CookingRecipe",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_CookingRecipe_RecipeCategory_RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe",
                column: "RecipeCategoryId",
                principalSchema: "public",
                principalTable: "RecipeCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
