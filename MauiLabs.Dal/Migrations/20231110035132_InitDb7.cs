using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MauiLabs.Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitDb7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                schema: "public",
                table: "IngredientsList");

            migrationBuilder.DropTable(
                name: "IngredientItem");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "public",
                table: "UserProfile");

            migrationBuilder.RenameTable(
                name: "RecipeCategory",
                newName: "RecipeCategory",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                newName: "IngredientUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientsList_IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                newName: "IX_IngredientsList_IngredientUnitId");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceLink",
                schema: "public",
                table: "UserProfile",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "public",
                table: "IngredientsList",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FriendList",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequesterId = table.Column<int>(type: "integer", nullable: false),
                    AddresseeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendList_UserProfile_AddresseeId",
                        column: x => x.AddresseeId,
                        principalSchema: "public",
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendList_UserProfile_RequesterId",
                        column: x => x.RequesterId,
                        principalSchema: "public",
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientUnit",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recommendation",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FromUserId = table.Column<int>(type: "integer", nullable: false),
                    ToUserId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendation_CookingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "public",
                        principalTable: "CookingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendation_UserProfile_FromUserId",
                        column: x => x.FromUserId,
                        principalSchema: "public",
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendation_UserProfile_ToUserId",
                        column: x => x.ToUserId,
                        principalSchema: "public",
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendList_AddresseeId",
                schema: "public",
                table: "FriendList",
                column: "AddresseeId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendList_Id",
                schema: "public",
                table: "FriendList",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendList_RequesterId",
                schema: "public",
                table: "FriendList",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_FromUserId",
                schema: "public",
                table: "Recommendation",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_Id",
                schema: "public",
                table: "Recommendation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_RecipeId",
                schema: "public",
                table: "Recommendation",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_ToUserId",
                schema: "public",
                table: "Recommendation",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsList_IngredientUnit_IngredientUnitId",
                schema: "public",
                table: "IngredientsList",
                column: "IngredientUnitId",
                principalSchema: "public",
                principalTable: "IngredientUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsList_IngredientUnit_IngredientUnitId",
                schema: "public",
                table: "IngredientsList");

            migrationBuilder.DropTable(
                name: "FriendList",
                schema: "public");

            migrationBuilder.DropTable(
                name: "IngredientUnit",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Recommendation",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "ReferenceLink",
                schema: "public",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "public",
                table: "IngredientsList");

            migrationBuilder.RenameTable(
                name: "RecipeCategory",
                schema: "public",
                newName: "RecipeCategory");

            migrationBuilder.RenameColumn(
                name: "IngredientUnitId",
                schema: "public",
                table: "IngredientsList",
                newName: "IngredientItemId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientsList_IngredientUnitId",
                schema: "public",
                table: "IngredientsList",
                newName: "IX_IngredientsList_IngredientItemId");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "public",
                table: "UserProfile",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IngredientItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Unit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientItem", x => x.Id);
                });

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
