using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiLabs.Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitDb6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationTime",
                schema: "public",
                table: "CookingRecipe",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                column: "IngredientItemId",
                principalTable: "IngredientItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                schema: "public",
                table: "IngredientsList");

            migrationBuilder.DropColumn(
                name: "PublicationTime",
                schema: "public",
                table: "CookingRecipe");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                column: "IngredientItemId",
                principalTable: "IngredientItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
