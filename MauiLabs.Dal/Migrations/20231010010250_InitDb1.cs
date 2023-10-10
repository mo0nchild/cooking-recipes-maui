using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MauiLabs.Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

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

            migrationBuilder.CreateTable(
                name: "RecipeCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authorization",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authorization_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalSchema: "public",
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CookingRecipe",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    RecipeCategoryId = table.Column<int>(type: "integer", nullable: false),
                    PublisherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingRecipe_RecipeCategory_RecipeCategoryId",
                        column: x => x.RecipeCategoryId,
                        principalTable: "RecipeCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookingRecipe_UserProfile_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "public",
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookmark",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    ProfileId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookmark_CookingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "public",
                        principalTable: "CookingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmark_UserProfile_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "public",
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    PublicationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProfileId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.CheckConstraint("Rating_Constraint", "\"Rating\" BETWEEN 0 AND 5");
                    table.ForeignKey(
                        name: "FK_Comment_CookingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "public",
                        principalTable: "CookingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_UserProfile_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "public",
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsList",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    IngredientItemId = table.Column<int>(type: "integer", nullable: false),
                    CookingRecipeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsList_CookingRecipe_CookingRecipeId",
                        column: x => x.CookingRecipeId,
                        principalSchema: "public",
                        principalTable: "CookingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsList_IngredientItem_IngredientItemId",
                        column: x => x.IngredientItemId,
                        principalTable: "IngredientItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_Id",
                schema: "public",
                table: "Authorization",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_Login",
                schema: "public",
                table: "Authorization",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_UserProfileId",
                schema: "public",
                table: "Authorization",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_Id",
                schema: "public",
                table: "Bookmark",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_ProfileId",
                schema: "public",
                table: "Bookmark",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_RecipeId",
                schema: "public",
                table: "Bookmark",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Id",
                schema: "public",
                table: "Comment",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProfileId",
                schema: "public",
                table: "Comment",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_RecipeId",
                schema: "public",
                table: "Comment",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingRecipe_Id",
                schema: "public",
                table: "CookingRecipe",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CookingRecipe_PublisherId",
                schema: "public",
                table: "CookingRecipe",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingRecipe_RecipeCategoryId",
                schema: "public",
                table: "CookingRecipe",
                column: "RecipeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsList_CookingRecipeId",
                schema: "public",
                table: "IngredientsList",
                column: "CookingRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsList_Id",
                schema: "public",
                table: "IngredientsList",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsList_IngredientItemId",
                schema: "public",
                table: "IngredientsList",
                column: "IngredientItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_Email",
                schema: "public",
                table: "UserProfile",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_Id",
                schema: "public",
                table: "UserProfile",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authorization",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Bookmark",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Comment",
                schema: "public");

            migrationBuilder.DropTable(
                name: "IngredientsList",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CookingRecipe",
                schema: "public");

            migrationBuilder.DropTable(
                name: "IngredientItem");

            migrationBuilder.DropTable(
                name: "RecipeCategory");

            migrationBuilder.DropTable(
                name: "UserProfile",
                schema: "public");
        }
    }
}
