using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogApplication.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Cathegories_CathegoryId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTag_Articles_ArticlesId",
                table: "ArticleTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTag_Tag_TagsId",
                table: "ArticleTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cathegories",
                table: "Cathegories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleTag",
                table: "ArticleTag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Cathegories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "ArticleTag",
                newName: "ArticleTags");

            migrationBuilder.RenameColumn(
                name: "CathegoryId",
                table: "Articles",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CathegoryId",
                table: "Articles",
                newName: "IX_Articles_CategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tags",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "ArticleTags",
                newName: "TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleTag_TagsId",
                table: "ArticleTags",
                newName: "IX_ArticleTags_TagsTagId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleTags",
                table: "ArticleTags",
                columns: new[] { "ArticlesId", "TagsTagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Articles_ArticlesId",
                table: "ArticleTags",
                column: "ArticlesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tags_TagsTagId",
                table: "ArticleTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Articles_ArticlesId",
                table: "ArticleTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tags_TagsTagId",
                table: "ArticleTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleTags",
                table: "ArticleTags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Cathegories");

            migrationBuilder.RenameTable(
                name: "ArticleTags",
                newName: "ArticleTag");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Articles",
                newName: "CathegoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                newName: "IX_Articles_CathegoryId");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "Tag",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TagsTagId",
                table: "ArticleTag",
                newName: "TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleTags_TagsTagId",
                table: "ArticleTag",
                newName: "IX_ArticleTag_TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cathegories",
                table: "Cathegories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleTag",
                table: "ArticleTag",
                columns: new[] { "ArticlesId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Cathegories_CathegoryId",
                table: "Articles",
                column: "CathegoryId",
                principalTable: "Cathegories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTag_Articles_ArticlesId",
                table: "ArticleTag",
                column: "ArticlesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTag_Tag_TagsId",
                table: "ArticleTag",
                column: "TagsId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
