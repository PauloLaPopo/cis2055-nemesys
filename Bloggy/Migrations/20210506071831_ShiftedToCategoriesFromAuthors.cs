using Microsoft.EntityFrameworkCore.Migrations;

namespace Bloggy.Migrations
{
    public partial class ShiftedToCategoriesFromAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Authors_AuthorId",
                table: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "BlogPosts",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_CategoryId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Categories_CategoryId",
                table: "BlogPosts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Categories_CategoryId",
                table: "BlogPosts");

            /*migrationBuilder.DropTable(
                name: "Authors");*/

            migrationBuilder.RenameColumn(
                name: "AuthorId2",
                table: "BlogPosts",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_AuthorId2",
                table: "BlogPosts",
                newName: "IX_BlogPosts_StatusId");

            migrationBuilder.CreateTable(
                name: "Statues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Statues_StatusId",
                table: "BlogPosts",
                column: "StatusId",
                principalTable: "Statues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Categories_CategoryId",
                table: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "BlogPosts",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_CategoryId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_AuthorId");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Authors_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);



            migrationBuilder.DropForeignKey(
               name: "FK_BlogPosts_Statues_StatusId",
               table: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Statues");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "BlogPosts",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_StatusId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_AuthorId2");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Authors_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
