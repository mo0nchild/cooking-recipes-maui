using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiLabs.Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitDb5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookingRecipe_RecipeCategory_RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                schema: "public",
                table: "IngredientsList");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CookingRecipe_RecipeCategory_RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe",
                column: "RecipeCategoryId",
                principalTable: "RecipeCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                column: "IngredientItemId",
                principalTable: "IngredientItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookingRecipe_RecipeCategory_RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                schema: "public",
                table: "IngredientsList");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
                principalTable: "RecipeCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                column: "IngredientItemId",
                principalTable: "IngredientItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
