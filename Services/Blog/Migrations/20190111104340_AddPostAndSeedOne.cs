using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class AddPostAndSeedOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("3f4cf919-884c-418b-bea3-bd400ace37fe"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("d8d603ae-b486-4d6a-92ab-5f7bb87c6495"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("e9f1ad0b-e281-44db-b4fc-c3c2a23919ff"));

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    BlogId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatorId", "Title" },
                values: new object[,]
                {
                    { new Guid("3bee4b2b-6426-4838-9ed0-b7a4d67feedc"), "Content-1", new Guid("a2d0f792-530e-47a6-a22d-5f5ea7f6ebe0"), "Title-1" },
                    { new Guid("c4752618-8374-42c8-9c40-6bff8ffbb027"), "Content-2", new Guid("6a6ac338-7a25-4049-a7c7-9de2b922ae80"), "Title-2" },
                    { new Guid("cff9d13f-ce97-40cd-a51d-1df08706b8cd"), "Content-3", new Guid("f796f035-d9da-4c87-8c9b-6a75074b46d1"), "Title-3" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BlogId", "Content", "CreatorId" },
                values: new object[] { new Guid("785e43e4-3400-49ff-a249-7a2c96d75890"), new Guid("3bee4b2b-6426-4838-9ed0-b7a4d67feedc"), "Content", new Guid("ee825f96-9522-4c6e-ae5c-383ad3f68668") });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("3bee4b2b-6426-4838-9ed0-b7a4d67feedc"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("c4752618-8374-42c8-9c40-6bff8ffbb027"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("cff9d13f-ce97-40cd-a51d-1df08706b8cd"));

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatorId", "Title" },
                values: new object[] { new Guid("e9f1ad0b-e281-44db-b4fc-c3c2a23919ff"), "Content-1", new Guid("7762e7eb-d979-499c-882d-bbe31e2bfc15"), "Title-1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatorId", "Title" },
                values: new object[] { new Guid("d8d603ae-b486-4d6a-92ab-5f7bb87c6495"), "Content-2", new Guid("6b072a48-9223-4e78-8269-5563015129ea"), "Title-2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatorId", "Title" },
                values: new object[] { new Guid("3f4cf919-884c-418b-bea3-bd400ace37fe"), "Content-3", new Guid("91b66212-7a01-4c13-bfe6-ba196d414fe0"), "Title-3" });
        }
    }
}
