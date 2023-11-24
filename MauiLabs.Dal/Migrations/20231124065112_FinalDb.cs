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
            migrationBuilder.DropColumn(
                name: "Confirmed",
                schema: "public",
                table: "CookingRecipe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                schema: "public",
                table: "CookingRecipe",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
